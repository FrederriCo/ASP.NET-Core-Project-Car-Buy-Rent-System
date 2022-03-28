namespace CarBuyRentSystem.Core.Models.Cars
{
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarBuyRentSystem.Infrastructure.Data.DataConstants.Car;
    public class AddCarFormServiceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength, ErrorMessage = ("The length mst be between {2} and {1}"))]
        public string Brand { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength, ErrorMessage = ("The length mst be between {2} and {1}"))]
        public string Model { get; set; }

        [Required(ErrorMessage = "Enter the year, please!")]
        [Range(YearMinValue, YearMaxValue, ErrorMessage = "Invalid year")]
        public int Year { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength, ErrorMessage = "Enter the url, please!")]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Enter a description, please!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select the category, please!")]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Select the fuel type, please!")]
        [EnumDataType(typeof(Fuel))]
        public Fuel Fuel { get; set; }

        [Required(ErrorMessage = "Select the transmission, please!")]
        [EnumDataType(typeof(Transmission))]
        public Transmission Transmission { get; set; }

        [Range(LugageMinValue, LugageMaxValue, ErrorMessage = "Invalid Lugage")]
        public int Lugage { get; set; }

        [Range(DoorsMinValue, DoorsMaxValue, ErrorMessage = "Invalid doors")]
        public int Doors { get; set; }

        [Range(PassagerMinValue, PassagerMaxValue, ErrorMessage = "Invalid Passager")]
        public int Passager { get; set; }

        [Required(ErrorMessage = "Enter price, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot enter negative values!")]
        [Display(Name = "Total price car")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter rent price, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot enter negative values!")]
        [Display(Name = "Price per day rent a car")]
        public decimal RentPricePerDay { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public IEnumerable<CarLocationServiceModel> Locations { get; set; }
    }
}
