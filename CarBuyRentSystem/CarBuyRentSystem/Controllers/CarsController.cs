namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Data;
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
            if (!ModelState.IsValid)
            {
                return View(new AddCarFormModel
                {
                    Locations = this.GetCarLocation()
                });
            }

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
