namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Services.Cars;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : AdminController
    {
        private readonly ICarService cars;
        private readonly IMapper mapper;

        public CarsController(ICarService cars, IMapper mapper)
        {
            this.cars = cars;
            this.mapper = mapper;
        }
        public IActionResult All() => View(this.cars.AdminGetAllCar());

        public IActionResult ChangeVisability(int id)
        {
            cars.ChangeVisability(id);

            return RedirectToAction(nameof(All));
        }
        
       
    }
}
