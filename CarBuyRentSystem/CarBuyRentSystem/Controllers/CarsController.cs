namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;    

    public class CarsController : Controller
    {
        private readonly CarDbContext db;

        public CarsController(CarDbContext db)
        {
            this.db = db;
        }
        public IActionResult Add() => View(new AddCarFormModel
        {
            Locations = this.GetCarLocation()
        });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
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
                LocatonId = car.LocationId

            };

            db.Cars.Add(carAdd);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

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
