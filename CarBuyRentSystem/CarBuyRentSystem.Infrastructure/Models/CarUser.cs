namespace CarBuyRentSystem.Infrastructure.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.DataConstants.CarUser;
    

    public class CarUser : IdentityUser
    {
        public string CarUsername { get; set; }
        
        [MaxLength(FullNameMaxLength)]
        public string Name { get; set; }

        
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }
               
        [Column(TypeName = DecimalDefaultValue)]
        public decimal Balance { get; set; }       

        public ICollection<BuyCar> OwnedCars { get; init; } = new List<BuyCar>();

        public ICollection<RentCar> CarsUnderRent { get; init; } = new List<RentCar>();
    }
}