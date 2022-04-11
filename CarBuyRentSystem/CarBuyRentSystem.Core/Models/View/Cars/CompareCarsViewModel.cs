namespace CarBuyRentSystem.Core.Models.View.Cars
{
    using System.ComponentModel.DataAnnotations;
    using CarBuyRentSystem.Infrastructure.Models;
    public class CompareCarsViewModel
    {
        [Required]
        public Car FirstCar { get; set; }

        [Required]
        public Car SecondCar { get; set; }
    }
}
