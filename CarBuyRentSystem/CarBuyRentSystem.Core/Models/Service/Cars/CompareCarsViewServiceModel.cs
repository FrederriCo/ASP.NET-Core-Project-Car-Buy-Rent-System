namespace CarBuyRentSystem.Core.Models.Cars
{   
    using CarBuyRentSystem.Infrastructure.Models;
    public class CompareCarsViewServiceModel
    {       
        public Car FirstCar { get; set; }
        
        public Car SecondCar { get; set; }
    }
}
