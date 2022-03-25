namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using CarBuyRentSystem.Core.Services.UserService;
    using CarBuyRentSystem.Core.Models.View.RentCars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Core.Models.Users;
    using CarBuyRentSystem.Core.Models.Cars;

    using static WebConstants;

    public class RentCarsController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ICarService carService;

        public RentCarsController(IUserService userService 
                                 ,IMapper mapper
                                 ,ICarService carService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.carService = carService;
           
        }

        [Authorize]
        public  IActionResult Rent(int id)
        {
            var getCar = this.carService.Details(id);

            var car = mapper.Map<CarDetailsServiceModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [Authorize]
        public async Task<IActionResult> MyRentCars()
        {
            var userRentCars = (await this.userService.GetAllRentedCarsByUser(this.User.GetId()))
                                .Select(mapper.Map<RentedCarsViewModel>);

            return View(userRentCars);
        }

        [Authorize]
        [HttpPost]                            
        public async Task<IActionResult> Rent(RentCarBindingModel model)
        {
            var userId = this.User.GetId();
            model.UserId = userId;

            var rentCar = mapper.Map<RentCar>(model);

            var result = await this.carService.Rent(rentCar, this.User.Identity.Name);

            if (!result)
            {
                return RedirectToAction("ApplicationError", "Home");
            }
           
            return RedirectToAction("MyRentCars", "RentCars");
        }

        [Authorize]
        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> RentedAllCars()
        {
            var rentedCars = (await this.userService.GetAllRentedCars())
                .Select(mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCars);
        }

    }
}
