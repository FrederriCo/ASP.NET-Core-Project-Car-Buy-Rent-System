namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;
    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Core.Services.UserService;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    using static WebConstants;

    public class BuyCarsController : Controller
    {
        private readonly IUserService userService;
        private readonly ICarService carService;
        private readonly IMapper mapper;

        public BuyCarsController(IUserService userService 
                                ,IMapper mapper
                                ,ICarService carService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.carService = carService;
        }
        
        [Authorize]
        public  IActionResult Buy(int id)
        {
            var getCar = this.carService.Details(id);

            var car = mapper.Map<CarServiceListingViewModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buy(BuyCarBindingModel model)
        {
            var buyCar = mapper.Map<BuyCar>(model);

            var result = await this.carService.Buy(buyCar, this.User.Identity.Name);

            if (!result)
            {
                return RedirectToAction("ApplicationError", "Home");
            }
            
            return RedirectToAction("MyBuyCars", "BuyCars");
        }


        [Authorize]
        public async Task<IActionResult> MyBuyCars()
        {
            var userId = this.User.GetId();
          //  var userName = this.User.Identity.Name();
            var boughtCarsByUser = (await this.userService.GetAllBoughtCarsByUser(userId))
                .Select(mapper.Map<SoldCarsViewModel>);

            return this.View(boughtCarsByUser);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> SoldCars()
        {
            var soldCars = (await this.userService.GetAllSoldCars())
                .Select(mapper.Map<SoldCarsViewModel>);

            return this.View(soldCars);
        }
    }
}
