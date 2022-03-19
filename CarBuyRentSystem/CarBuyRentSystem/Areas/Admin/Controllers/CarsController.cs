namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using CarBuyRentSystem.Core.Services.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    public class CarsController : AdminController
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }
        public IActionResult All() => View(this.cars.AllCarBrands());
    }
}
