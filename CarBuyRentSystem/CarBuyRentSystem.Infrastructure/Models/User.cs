namespace CarBuyRentSystem.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Balance { get; set; }       

        public ICollection<BuyCar> OwnedCars { get; init; } = new List<BuyCar>();

        public ICollection<RentCar> CarsUnderRent { get; init; } = new List<RentCar>();
    }
}