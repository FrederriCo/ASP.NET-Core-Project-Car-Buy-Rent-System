namespace CarBuyRentSystem.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarBuyRentSystem.Core.Models;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Core.Services.Dealrs;

    using static Infrastructure.Data.WebConstants;

    public class DealersController : Controller
    {
        private readonly IDealerService dealers;

        public DealersController(IDealerService dealers)
              => this.dealers = dealers;
        
        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(DealerFormServiceModel dealer)
        {
            var userId = this.User.GetId();

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var userIdAlreadyDealer = await dealers.Create(dealer, userId);

            if (!userIdAlreadyDealer)
            {
                RedirectToAction("ApplicationError", "Home");
            }            

            TempData[GlobalMessageKey] = WelcomeMessageDealer;

            return RedirectToAction("All", "Cars");
        }
       
    }
}
