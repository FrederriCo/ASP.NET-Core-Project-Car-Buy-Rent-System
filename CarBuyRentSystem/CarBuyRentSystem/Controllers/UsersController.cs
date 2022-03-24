namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Models.Users;
    using CarBuyRentSystem.Core.Services.UserService;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> UserRents()
        {
            var userRentCars = (await this.userService.GetAllRentedCarsByUser(this.User.Identity.Name))
                                .Select(mapper.Map<RentedCarViewModel>);


            return View(userRentCars);
        }
    }
}
