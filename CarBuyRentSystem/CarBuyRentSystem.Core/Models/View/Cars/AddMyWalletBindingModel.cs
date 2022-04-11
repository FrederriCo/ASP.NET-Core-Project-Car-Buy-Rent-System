namespace CarBuyRentSystem.Core.Models.View.Cars
{
    using System.ComponentModel.DataAnnotations;
    public class AddMyWalletBindingModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Enter correct value, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot transfer negative or values less than 1 dollar!")]
        public decimal Balance { get; set; }
    }
}
