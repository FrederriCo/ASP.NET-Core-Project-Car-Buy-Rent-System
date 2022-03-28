namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using System.Linq;
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
    using System.Threading.Tasks;

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
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return View(new AddCarFormServiceModel
            {
                Locations = this.cars.AllCarLocation()
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddCarFormServiceModel car)
        {
            var dealerId = this.dealers.GetDealerId(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!this.cars.LocationExsts(car.LocationId))
            {
                this.ModelState.AddModelError(nameof(car.LocationId), "Location does not exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(new AddCarFormServiceModel
                {
                    Locations = this.cars.AllCarLocation()
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
       
        public IActionResult All([FromQuery] AllCarsViewModel query)
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

            var cars = this.cars.GetCars(carsQuery
                .Skip((query.CurentPage - 1) * AllCarsViewModel.CarPerPage)
                .Take(AllCarsViewModel.CarPerPage));


            var carBrands = this.cars.AllCarBrands();

            query.Brands = carBrands;
            query.Cars = cars;
            query.TotalCars = totalCars;

            return View(query);

        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            var cars =  this.cars.Details(id);

            if (cars.UserId != userId && !User.IsAdmin())
            {
                return BadRequest();
            }

            //Add Auto Mapper

            var carForm = this.mapper.Map<AddCarFormServiceModel>(cars);
            carForm.Locations = this.cars.AllCarLocation(); // For Collection Atuo Mapper

            return  View(carForm);           
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CreateCarServiceModel car)
        {
            var userId = this.User.GetId();

            var dealerId = this.dealers.GetDealerId(userId);

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!this.cars.LocationExsts(car.LocationId))
            {
                this.ModelState.AddModelError(nameof(car.LocationId), "Location does not exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(new CreateCarServiceModel
                {
                    Locations = this.cars.AllCarLocation()
                });
            }

            var carData = this.db.Cars.Find(car.Id);

            if (!this.cars.IsByDealer(car.Id, dealerId) && !User.IsAdmin())
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

        public IActionResult Delete(int id)
        {
            var getCar = cars.GetCarId(id);

            // var car = mapper.Map<CreateCarServiceModel>(getCar);

            if (getCar == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            cars.Delete(id);

            if (User.IsAdmin())
            {
                return RedirectToAction("All", "Cars", new { area = "Admin" });
            }

            TempData[GlobalMessageKey] = "Your Car success delited";

            return RedirectToAction(nameof(DealerCar));

        }

        public IActionResult Details(int id)
        {
            var getCar = cars.GetCarId(id);

            var car = mapper.Map<CarServiceListingViewModel>(getCar);

            if (car == null)
            {
                return BadRequest();
            }


            return View(car);
        }

    }
}
