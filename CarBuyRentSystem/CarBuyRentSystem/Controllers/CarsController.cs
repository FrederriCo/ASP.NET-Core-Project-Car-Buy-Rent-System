namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(string brand, string search)
        {
            var carsQuery = this.db.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                carsQuery = carsQuery.Where(c =>
                        c.Brand.ToLower().Contains(search.ToLower())
                        || c.Model.ToLower().Contains(search.ToLower())
                        || (c.Model + c.Brand).ToLower().Contains(search.ToLower())                        
                        || c.Description.ToLower().Contains(search.ToLower())
                    );
             }

            var cars = carsQuery                
                .OrderByDescending(c => c.Id)
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

            var carBrands = db
                        .Cars
                        .Select(c => c.Brand)
                        .Distinct()
                        .ToList();


            return View(new AllCarsViewModel
            {
                Brands = carBrands,
                Cars = cars,
                Search = search
                
            });
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
