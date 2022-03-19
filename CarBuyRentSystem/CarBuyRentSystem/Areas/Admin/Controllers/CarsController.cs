namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using CarBuyRentSystem.Core.Services.Cars;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : AdminController
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }
        public IActionResult All() => View(this.cars.AdminGetAllCar());

        public IActionResult ChangeVisability(int id)
        {
            cars.ChangeVisability(id);

            return RedirectToAction(nameof(All));
        }
    }
}
