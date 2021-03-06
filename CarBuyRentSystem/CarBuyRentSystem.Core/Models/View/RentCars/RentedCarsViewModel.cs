namespace CarBuyRentSystem.Core.Models.View.RentCars
{
    using System;

    using CarBuyRentSystem.Infrastructure.Models;

    public class RentedCarsViewModel
    {
        public string UserId { get; set; }

        public string CarUser { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
