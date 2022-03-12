namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;
    using CarBuyRentSystem.Models.Cars.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly CarDbContext db;
        private readonly ICarService cars;

        public CarsController(CarDbContext db, ICarService cars)
        {
            this.db = db;
            this.cars = cars;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!UserIsDealer())
            {           
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            return View(new AddCarFormModel
            {
                Locations = this.GetCarLocation()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            var dealerId = this.db
                            .Dealers
                            .Where(x => x.UserId == this.User.GetId())
                            .Select(x => x.Id)
                            .FirstOrDefault();

            if (dealerId == 0)
            {              
                return RedirectToAction(nameof(DealersController.Create), "Dealers");
            }

            if (!db.Locations.Any(x => x.Id == car.LocationId))
            {
                this.ModelState.AddModelError(nameof(car.LocationId), "Location does not exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(new AddCarFormModel
                {
                    Locations = this.GetCarLocation()
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

            var cars = carsQuery
                .Skip((query.CurentPage - 1) * AllCarsViewModel.CarPerPage)
                .Take(AllCarsViewModel.CarPerPage)
                .Select(c => new CarListingVIewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Category = c.Category,
                    Fuel = c.Fuel,
                    Transmission = c.Transmission,
                    ImageUrl = c.ImageUrl,
                    Lugage = c.Lugage,
                    Doors = c.Doors,
                    Passager = c.Passager,
                    Locaton = c.Location.Name,
                    Price = c.Price,
                    RentPricePerDay = c.RentPricePerDay
                })
                .ToList();

            var carBrands = this.cars.AllCarBrands();

            query.Brands = carBrands;
            query.Cars = cars;
            query.TotalCars = totalCars;

            return View(query);

        }


        private bool UserIsDealer()
            => this.db
                .Dealers
                .Any(d => d.UserId == this.User.GetId());
        private IEnumerable<CarLocationViewModel> GetCarLocation()
         => this.db
            .Locations
            .Select(l => new CarLocationViewModel
            {
                Id = l.Id,
                Name = l.Name
            })
            .ToList();
    }
}
