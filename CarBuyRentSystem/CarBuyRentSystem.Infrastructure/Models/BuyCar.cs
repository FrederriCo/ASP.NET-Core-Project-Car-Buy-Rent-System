namespace CarBuyRentSystem.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.DataConstants.Car;

    public class BuyCar
    {
        public int BuyCarId { get; set; }

        public string UserId { get; set; }

        public CarUser User { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        [Column(TypeName = DecimalDefaultValue)]
        public decimal Price { get; set; }

        public DateTime BoughtOn { get; set; }
    }
}