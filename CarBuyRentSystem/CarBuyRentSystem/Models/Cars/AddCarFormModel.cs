namespace CarBuyRentSystem.Models.Cars
{
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarBuyRentSystem.Infrastructure.Data.DataConstants.Car;
    public class AddCarFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [EnumDataType(typeof(Category))]
        public Category? Category { get; init; }

        [Required]
        [EnumDataType(typeof(Fuel))]
        public Fuel? Fuel { get; init; }

        [Required]
        [EnumDataType(typeof(Transmission))]
        public Transmission? Transmission { get; init; }

        [Range(LugageMinValue, LugageMaxValue)]
        public int Lugage { get; init; }

        [Range(PassagerMinValue, PassagerMaxValue)]
        public int Passager { get; init; }

        [Required]
        [Range(1, double.MaxValue)]
        [Display(Name = "Total price car")]
        public decimal? Price { get; init; }

        [Required]
        [Range(1, double.MaxValue)]
        [Display(Name = "Price per day rent a car")]
        public decimal? RentPricePerDay { get; init; }

        [Display(Name = "Location")]
        public int LocationId { get; init; }

       // public IEnumerable<CarLocaServiceModel> Categories { get; set; }
    }
}
