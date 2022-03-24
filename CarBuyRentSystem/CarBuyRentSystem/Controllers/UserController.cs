namespace CarBuyRentSystem.Controllers
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Services.UserService;
    using Microsoft.AspNetCore.Mvc;
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
    }
}
