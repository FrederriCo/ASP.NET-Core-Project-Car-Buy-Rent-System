namespace CarBuyRentSystem.Core.Models.View.RentCars
{
    using System;

    using CarBuyRentSystem.Infrastructure.Models;
    using System.ComponentModel.DataAnnotations;

    using static WebConstants;

    public class RentCarBindingModel
    {
        public int RentCarId { get; set; }

        public string UserId { get; set; }

        public CarUser User { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = DataFormatStringDefault, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = DataFormatStringDefault, ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
