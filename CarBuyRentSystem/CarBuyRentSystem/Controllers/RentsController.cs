namespace CarBuyRentSystem.Controllers
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    using CarBuyRentSystem.Core.Services.UserService;
    using CarBuyRentSystem.Core.Models.Users;

    using static WebConstants;

    public class RentsController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public RentsController(IUserService userService, IMapper mapper)
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
        [Authorize(Roles = AdministratorRoleName)]
        public async Task<IActionResult> RentedAllCars()
        {
            var rentedCars = (await this.userService.GetAllRentedCars())
                .Select(mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCars);
        }


    }
}
