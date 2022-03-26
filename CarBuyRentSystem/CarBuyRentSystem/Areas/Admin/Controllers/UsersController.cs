using CarBuyRentSystem.Core.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IActionResult> AllUsers() => View(await this.userService.GetAllUser());
    }
}
