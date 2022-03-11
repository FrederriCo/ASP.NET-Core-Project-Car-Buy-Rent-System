namespace CarBuyRentSystem.Controllers
{
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

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

            return RedirectToAction("All", "Cars");
        }
       
    }
}
