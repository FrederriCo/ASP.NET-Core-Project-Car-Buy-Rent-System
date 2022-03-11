namespace CarBuyRentSystem.Infrastructure.Models
{
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Data.DataConstants.Car;
    public class Car
    {
       public int Id { get; init; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public Category Category { get; set; }

        public Fuel Fuel { get; set; }

        public Transmission Transmission { get; set; }

        public int Lugage { get; set; }

        public int Doors { get; set; }

        public int Passager { get; set; }

        [Column(TypeName = DecimalDefaultValue)]
        public decimal RentPricePerDay { get; set; }

        [Column(TypeName = DecimalDefaultValue)]
        public decimal Price { get; set; }

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }
        
        public int LocationId { get; set; }

        public Location Location { get; init; }

        public ICollection<BuyCar> Owners { get; init; } = new List<BuyCar>();

        public ICollection<RentCar> Renters { get; init; } = new List<RentCar>();

    }
}
