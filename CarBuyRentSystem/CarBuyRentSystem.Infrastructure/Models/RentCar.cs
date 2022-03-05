namespace CarBuyRentSystem.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RentCar
    {
        public int RentCarId { get; set; }

        [Required]
        public string UserId { get; set; }

        public CarUser CarUser { get; set; }
        
        public int CarId { get; set; }

        public Car Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}