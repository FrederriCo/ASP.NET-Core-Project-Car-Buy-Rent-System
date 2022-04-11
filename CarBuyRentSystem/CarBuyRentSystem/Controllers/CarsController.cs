namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Core.Services.Dealrs;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.Service.Cars;

    using static Infrastructure.Data.WebConstants;

    public class CarsController : Controller
    {
        private readonly ICarService carsService;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;

        public CarsController(ICarService carService,
                              IDealerService dealerService,
                              IMapper mapper)
        {
            this.carsService = carService;
            this.dealerService = dealerService;
            this.mapper = mapper;
        }


        [Authorize]
        public async Task<IActionResult> Add()
        {
            var isDealer = await this.dealerService.IsDealer(this.User.GetId());

            if (!isDealer)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return View(new AddCarFormServiceModel
            {
                Locations = await this.carsService.AllCarLocation()
            });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddCarFormServiceModel car)
        {
            var dealerId = await dealerService.GetDealerId(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var locationExists = await this.carsService.LocationExists(car.LocationId);

            if (!locationExists)
            {
                this.ModelState.AddModelError(nameof(car.LocationId), LocationNotExists);
            }

            if (!ModelState.IsValid)
            {
                return View(new AddCarFormServiceModel
                {
                    Locations = await this.carsService.AllCarLocation()
                });
            }

            var carAdd = mapper.Map<Car>(car);
            carAdd.DealerId = dealerId;

            await this.carsService.Add(carAdd);

            TempData[GlobalMessageKey] = SuccessCreatedCar;

            return RedirectToAction(nameof(DealerCars));
        }

        [Authorize]
        public async Task<IActionResult> DealerCars()
        {
            var delarCars = await this.carsService.ByUser(this.User.GetId());

            return View(delarCars);
        }

        public async Task<IActionResult> All([FromQuery] AllCarsViewModel query)
        {
            var queryResult = await this.carsService.All(
                  query.Brand,
                  query.Search,
                  query.Sorting,
                  query.CurentPage,
                  AllCarsViewModel.CarPerPage);

            var carBrands = await this.carsService.AllCarBrands();

            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetId();

            var isDealer = await this.dealerService.IsDealer(userId);

            if (!isDealer && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var cars = await this.carsService.Details(id);            

            if (cars.UserId != userId && !User.IsAdmin())
            {
                return BadRequest();
            }

            var carForm = this.mapper.Map<AddCarFormServiceModel>(cars);

            carForm.Locations = await this.carsService.AllCarLocation();

            return View(carForm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CreateCarServiceModel car)
        {
            var userId = this.User.GetId();

            var dealerId = await this.dealerService.GetDealerId(userId);

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }
            var locationExists = await this.carsService.LocationExists(car.LocationId);

            if (!locationExists)
            {
                this.ModelState.AddModelError(nameof(car.LocationId), LocationNotExists);
            }

            if (!ModelState.IsValid)
            {
                return View(new CreateCarServiceModel
                {
                    Locations = await this.carsService.AllCarLocation()
                });
            }

            var carData = await this.carsService.GetCarId(car.Id);

            var isByDealer = await this.carsService.IsByDealer(car.Id, dealerId);

            if (!isByDealer && !User.IsAdmin())
            {
                return BadRequest();
            }

            if (carData.DealerId != dealerId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var carEdit = this.mapper.Map<CreateCarServiceModel>(car);

            carData.IsPublic = this.User.IsAdmin();

            await this.carsService.Edit(carEdit);

            TempData[GlobalMessageKey] = SuccessEditedCar;

            return RedirectToAction(nameof(DealerCars));
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var getCar = await carsService.GetCarId(id);

            if (getCar == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            await carsService.Delete(id);

            if (User.IsAdmin())
            {
                return RedirectToAction("All", "Cars", new { area = "Admin" });
            }

            TempData[GlobalMessageKey] = SuccessDelitedCar;

            return RedirectToAction(nameof(DealerCars));
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var getCar = await carsService.GetCarId(id);          

            if (getCar == null)
            {
                return BadRequest();
            }

            return View(getCar);
        }

        public async Task<IActionResult> CompareCars(int firstCarId, int secondCarId)
        {
            var getCars = await this.carsService.CompareCars(firstCarId, secondCarId);

            var carsToCompare = mapper.Map<CompareCarsViewModel>(getCars);

            if (carsToCompare == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(carsToCompare);
        }

        [Authorize]
        public IActionResult MyWallet()
            => this.View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MyWallet(AddMyWalletBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var result = mapper.Map<AddMyWalletServiceModel>(model);

            await this.carsService.AddBalance(result, this.User.Identity.Name);

            return RedirectToAction("MyWallet", "Cars");
        }
    }
}
