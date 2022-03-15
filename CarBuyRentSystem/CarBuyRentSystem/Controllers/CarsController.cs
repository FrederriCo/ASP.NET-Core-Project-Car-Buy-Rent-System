namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Core.Services.Dealrs;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;
    using CarBuyRentSystem.Models.Cars.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly CarDbContext db;
        private readonly ICarService cars;
        private readonly IDealerService dealers;

        public CarsController(CarDbContext db, ICarService cars, IDealerService dealers)
        {
            this.db = db;
            this.cars = cars;
            this.dealers = dealers;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return View(new AddCarFormModel
            {
                Locations = this.cars.AllCarLocation()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
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
                return View(new AddCarFormModel
                {
                    Locations = this.cars.AllCarLocation()
                });
            }

            var carAdd = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                ImageUrl = car.ImageUrl,
                Description = car.Description,
                Category = car.Category,
                Fuel = car.Fuel,
                Transmission = car.Transmission,
                Lugage = car.Lugage,
                Doors = car.Doors,
                Passager = car.Passager,
                RentPricePerDay = car.RentPricePerDay,
                Price = car.Price,
                LocationId = car.LocationId,
                DealerId = dealerId

            };

            db.Cars.Add(carAdd);
            db.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult DealerCar()
        {
            var delarCars = this.cars.ByUser(this.User.GetId());

            return View(delarCars);
        }
        public IActionResult All([FromQuery] AllCarsViewModel query)
        {
            var carsQuery = this.db.Cars.AsQueryable();

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

        }

    }
}
