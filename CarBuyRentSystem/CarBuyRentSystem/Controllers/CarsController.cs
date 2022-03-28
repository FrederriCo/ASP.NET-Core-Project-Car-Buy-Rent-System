namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Core.Services.Dealrs;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars.Enums;

    using static Infrastructure.Data.WebConstants;

    public class CarsController : Controller
    {
        private readonly CarDbContext db;
        private readonly ICarService cars;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public CarsController(CarDbContext db,
            ICarService carService,
            IDealerService dealerService,
            IMapper mapper)
        {
            this.db = db;
            this.cars = carService;
            this.dealers = dealerService;
            this.mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            var isDealer = await this.dealers.IsDealer(this.User.GetId());

            if (!isDealer)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return View(new AddCarFormServiceModel
            {
                Locations = await this.cars.AllCarLocation()
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddCarFormServiceModel car)
        {
            var dealerId =  await dealers.GetDealerId(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var locationExists = await this.cars.LocationExists(car.LocationId);

            if (!locationExists)
            {
                this.ModelState.AddModelError(nameof(car.LocationId), "Location does not exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(new AddCarFormServiceModel
                {
                    Locations = await this.cars.AllCarLocation()
                });
            }

            var carAdd = mapper.Map<Car>(car);
            carAdd.DealerId = dealerId;

            await this.cars.Add(carAdd);

            TempData[GlobalMessageKey] = SuccessCreatedCar;

            return RedirectToAction(nameof(DealerCar));
        }

        [Authorize]
        public async Task<IActionResult> DealerCar()
        {
            var delarCars = await this.cars.ByUser(this.User.GetId());

            return View(delarCars);
        }

        public async Task<IActionResult> All([FromQuery] AllCarsViewModel query)
        {
            var carsQuery = this.db.Cars.Where(c => c.IsPublic).AsQueryable();

            if (User.IsAdmin())
            {
                carsQuery = this.db.Cars.AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                carsQuery = carsQuery.Where(c =>
                        (c.Model + c.Brand).ToLower().Contains(query.Search.ToLower())
                        || c.Description.ToLower().Contains(query.Search.ToLower())
                    );
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.Price => carsQuery.OrderByDescending(x => x.RentPricePerDay),
                CarSorting.BrandAnyModel => carsQuery.OrderBy(x => x.Brand).ThenBy(c => c.Model),
                CarSorting.Year or _ => carsQuery.OrderByDescending(c => c.Year)
            };

            var totalCars = carsQuery.Count();

            var cars = await  this.cars.GetCars(carsQuery
                .Skip((query.CurentPage - 1) * AllCarsViewModel.CarPerPage)
                .Take(AllCarsViewModel.CarPerPage));


            var carBrands = await this.cars.AllCarBrands();

            query.Brands = carBrands;
            query.Cars = cars;
            query.TotalCars = totalCars;

            return View(query);

        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetId();
            var isDealer = await  this.dealers.IsDealer(userId);

            if (!isDealer && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var cars = await this.cars.Details(id);
               
            if (cars.UserId != userId && !User.IsAdmin())
            {
                return BadRequest();
            }

            //Add Auto Mapper

            var carForm = this.mapper.Map<AddCarFormServiceModel>(cars);
            carForm.Locations = await this.cars.AllCarLocation(); // For Collection Atuo Mapper

            return View(carForm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CreateCarServiceModel car)
        {
            var userId =  this.User.GetId();

            var dealerId = await this.dealers.GetDealerId(userId);

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }
            var locationExists = await this.cars.LocationExists(car.LocationId);

            if (!locationExists)
            {
                this.ModelState.AddModelError(nameof(car.LocationId), "Location does not exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(new CreateCarServiceModel
                {
                    Locations = await this.cars.AllCarLocation()
                });
            }

            var carData = await this.db.Cars.FindAsync(car.Id);

            var isByDealer = await this.cars.IsByDealer(car.Id, dealerId);

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

            await this.cars.Edit(carEdit);

            //carData.Brand = car.Brand;
            //carData.Model = car.Model;
            //carData.Year = car.Year;
            //carData.ImageUrl = car.ImageUrl;
            //carData.Description = car.Description;
            //carData.Category = car.Category;
            //carData.Fuel = car.Fuel;
            //carData.Transmission = car.Transmission;
            //carData.Lugage = car.Lugage;
            //carData.Doors = car.Doors;
            //carData.Passager = car.Passager;
            //carData.RentPricePerDay = car.RentPricePerDay;
            //carData.Price = car.Price;
            //carData.LocationId = car.LocationId;
            //carData.IsPublic = this.User.IsAdmin();

            //this.db.SaveChanges();

            TempData[GlobalMessageKey] = SuccessEditedCar;

            return RedirectToAction(nameof(DealerCar));
        }

        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var getCar = await cars.GetCarId(id);           

            if (getCar == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            await cars.Delete(id);

            if (User.IsAdmin())
            {
                return RedirectToAction("All", "Cars", new { area = "Admin" });
            }

            TempData[GlobalMessageKey] = "Your Car success delited";

            return RedirectToAction(nameof(DealerCar));

        }

        public async Task<IActionResult> Details(int id)
        {
            var getCar = await cars.GetCarId(id);

            var car = mapper.Map<CarServiceListingViewModel>(getCar);

            if (car == null)
            {
                return BadRequest();
            }

            return View(car);
        }

    }
}
