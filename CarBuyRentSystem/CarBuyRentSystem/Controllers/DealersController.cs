namespace CarBuyRentSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
   
    using CarBuyRentSystem.Models.Dealers;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;

    using static Infrastructure.Data.WebConstants;

    public class DealersController : Controller
    {
        private readonly CarDbContext db;

        public DealersController(CarDbContext db)
        {
            this.db = db;
        }
        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(DealerFormModel dealer)
        {
            var userId = this.User.GetId();

            var userIdAlreadyDealer = this.db
                                      .Dealers
                                      .Any(x => x.UserId == userId);

            if (userIdAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerAdd = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            db.Dealers.Add(dealerAdd);
            db.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you becomming a dealer!";

            return RedirectToAction("All", "Cars");
        }
       
    }
}
