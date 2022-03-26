namespace CarBuyRentSystem.Core.Models.Cars
{

    public class CarDetailsServiceModel : CarServiceListingViewModel
    {      
         public int DealerId { get; set; }

        public string  DealerName { get; set; }       

        public string UserId { get; set; }
    }
}
