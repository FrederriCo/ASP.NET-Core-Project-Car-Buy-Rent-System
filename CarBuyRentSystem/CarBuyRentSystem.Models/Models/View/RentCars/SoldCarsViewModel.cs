namespace CarBuyRentSystem.Core.Models.View.Users
{
    using System;
    using CarBuyRentSystem.Infrastructure.Models;
    public class SoldCarsViewModel
    {
        public string UserId { get; set; }

        public string User { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public decimal Price { get; set; }

        public DateTime BoughtOn { get; set; }
    }
}
