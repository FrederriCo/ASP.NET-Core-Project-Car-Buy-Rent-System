namespace CarBuyRentSystem.Core.Models
{
    using System.ComponentModel.DataAnnotations; 

    using static CarBuyRentSystem.Infrastructure.Data.DataConstants.Dealer;
    public class DealerFormServiceModel
    {
        [Required]
        [StringLength(NameDealerMaxLength, MinimumLength = NameDealerMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }       
    }
}
