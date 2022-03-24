namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Models.View.Users;
    using CarBuyRentSystem.Core.Services.UserService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BuyCarsController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public BuyCarsController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> BuyCars()
        {
            var boughtCarsByUser = (await this.userService.GetAllBoughtCarsByUser(this.User.Identity.Name))
                .Select(Mapper.Map<SoldCarsViewModel>);

            return this.View(boughtCarsByUser);
        }
    }
}
