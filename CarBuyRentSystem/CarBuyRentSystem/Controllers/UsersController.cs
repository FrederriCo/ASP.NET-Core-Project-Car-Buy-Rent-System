namespace CarBuyRentSystem.Controllers
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using CarBuyRentSystem.Core.Models.Users;
    using CarBuyRentSystem.Core.Models.View.Users;
    using CarBuyRentSystem.Core.Services.UserService;
   

    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> RentCars()
        {
            var userRentCars = (await this.userService.GetAllRentedCarsByUser(this.User.Identity.Name))
                                .Select(mapper.Map<RentedCarsViewModel>);


            return View(userRentCars);
        }

        [Authorize]        
        public async Task<IActionResult> BuyCars()
        {
            var boughtCarsByUser = (await this.userService.GetAllBoughtCarsByUser(this.User.Identity.Name))
                .Select(mapper.Map<SoldCarsViewModel>);

            return this.View(boughtCarsByUser);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RentedAllCars()
        {
            var rentedCars = (await this.userService.GetAllRentedCars())
                .Select(mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCars);
        }
    }
}
