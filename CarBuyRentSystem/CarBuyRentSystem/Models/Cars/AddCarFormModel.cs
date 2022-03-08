namespace CarBuyRentSystem.Models.Cars
{   
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarBuyRentSystem.Infrastructure.Data.DataConstants.Car;
    public class AddCarFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength, ErrorMessage = ("The length mst be between {2} and {1}"))]
        public string Brand { get; init; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength, ErrorMessage = ("The length mst be between {2} and {1}"))]
        public string Model { get; init; }

        [Required(ErrorMessage = "Enter the year, please!")]
        [Range(YearMinValue, YearMaxValue, ErrorMessage = "Invalid year")]
        public int Year { get; init; }

        [Required]
        [MaxLength(ImageUrlMaxLength, ErrorMessage = "Enter the url, please!")]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Enter a description, please!")]
        public string Description { get; init; }

        [Required(ErrorMessage = "Select the category, please!")]
        [EnumDataType(typeof(Category))]
        public Category? Category { get; init; }

        [Required(ErrorMessage = "Select the fuel type, please!")]
        [EnumDataType(typeof(Fuel))]
        public Fuel? Fuel { get; init; }

        [Required(ErrorMessage = "Select the transmission, please!")]
        [EnumDataType(typeof(Transmission))]
        public Transmission? Transmission { get; init; }

        [Range(LugageMinValue, LugageMaxValue, ErrorMessage =  "Invalid Lugage")]
        public int Lugage { get; init; }

        [Range(PassagerMinValue, PassagerMaxValue, ErrorMessage = "Invalid Passager")]
        public int Passager { get; init; }

        [Required(ErrorMessage = "Enter price, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot enter negative values!")]
        [Display(Name = "Total price car")]
        public decimal? Price { get; init; }

        [Required(ErrorMessage = "Enter rent price, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot enter negative values!")]
        [Display(Name = "Price per day rent a car")]
        public decimal? RentPricePerDay { get; init; }

        [Display(Name = "Location")]
        public int LocationId { get; init; }

        public IEnumerable<CarLocationViewModel> Locations { get; set; }
    }
}
