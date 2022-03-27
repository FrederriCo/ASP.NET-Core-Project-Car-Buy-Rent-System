namespace CarBuyRentSystem.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.DataConstants.Car;

    public class RentCar
    {
        public int RentCarId { get; set; }

        public string UserId { get; set; }

        public CarUser CarUser { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = DecimalDefaultValue)]
        public decimal TotalPrice { get; set; }
    }
}