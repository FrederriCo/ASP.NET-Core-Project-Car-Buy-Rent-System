namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Models;
    using CarBuyRentSystem.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly CarDbContext db;

        public HomeController(CarDbContext db)
        {
            this.db = db;
        }

        
        public IActionResult Index()
        {
            
            var cars = db
               .Cars
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
               .Take(3)
               .ToList();

            return View(cars);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
       
        
    }
}
