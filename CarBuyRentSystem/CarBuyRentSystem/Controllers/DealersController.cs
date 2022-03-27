namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Core.Models;
    using CarBuyRentSystem.Core.Services.Dealrs;
    using CarBuyRentSystem.Infrastructure.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(DealerFormServiceModel dealer)
        {
            var userId = this.User.GetId();

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var userIdAlreadyDealer = dealers.Create(dealer, userId);

            if (!userIdAlreadyDealer)
            {
                RedirectToAction("ApplicationError", "Home");
            }

            

            TempData[GlobalMessageKey] = WelcomeMessageDealer;

            return RedirectToAction("All", "Cars");
        }
       
    }
}
