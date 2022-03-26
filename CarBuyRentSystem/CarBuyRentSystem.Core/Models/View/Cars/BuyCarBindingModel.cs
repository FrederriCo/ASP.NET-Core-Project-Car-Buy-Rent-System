namespace CarBuyRentSystem.Core.Models.View.Cars
{
    using System;

    using CarBuyRentSystem.Infrastructure.Models;
    public class BuyCarBindingModel
    {
        public int BuyCarId { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public string UserId { get; set; }

        public CarUser User { get; set; }

        public decimal Price { get; set; }

        public DateTime BoughtOn { get; set; }
    }
}
