namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Core.Services.UserService;
    using CarBuyRentSystem.Core.Models.View.RentCars;   

    public class CarsController : AdminController
    {
        private readonly ICarService cars;
        private readonly IUserService userService;
        private readonly IMapper mapper;      

        public CarsController(ICarService cars
                              ,IUserService userService
                             ,IMapper mapper)                              
        {
            this.cars = cars;
            this.userService = userService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var total = cars.Total();

           return View(total);
        }

        public IActionResult All() => View(this.cars.AdminGetAllCar());

        public IActionResult Edit() => View(this.cars.AdminGetAllCar());       

        public IActionResult Delete() => View(this.cars.AdminGetAllCar());

        public IActionResult AllCars() => View(this.cars.AdminGetAllCar());       

        public IActionResult ChangeVisability(int id)
        {
            cars.ChangeVisability(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> RentedCars()
        {
            var rentedCars = (await this.userService.GetAllRentedCars())
                .Select(mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCars);
        }

        public async Task<IActionResult> SoldCars()
        {
            var soldCars = (await this.userService.GetAllSoldCars())
                .Select(mapper.Map<SoldCarsViewModel>);

            return this.View(soldCars);
        }

    }
}
