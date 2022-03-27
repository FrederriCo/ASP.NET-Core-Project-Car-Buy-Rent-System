using CarBuyRentSystem.Core.Services.UserService;
using CarBuyRentSystem.Infrastructure.Data;
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

        public async Task<IActionResult> AllDealers() => View(await this.userService.GetAllDealer());

        public async Task<IActionResult> DeleteUser(string id)
        {
            var userDelete = await userService.DeleteUser(id);

            if (userDelete)
            {
                return RedirectToAction("ApplicationError");
            }

            return RedirectToAction("AllUsers", "Users");
        }

        public async Task<IActionResult> DeleteDealer(int id)
        {
            var dealerDelete = await userService.DeleteDealrs(id);

            if (!dealerDelete)
            {
                return RedirectToAction("ApplicationError");
            }

            return RedirectToAction("AllDealers", "Users");
        }

        public IActionResult ApplicationError()
        {
            return View();
        }

    }
}
