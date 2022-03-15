using CarBuyRentSystem.Infrastructure.Models.Enums;

namespace CarBuyRentSystem.Core.Models.Cars
{
   
    public class CarDetailsServiceModel : CarServiceListingViewModel
    {      
        public string Description { get; set; }

        public int DealerId { get; set; }

        public string  DealerName { get; set; }
        public int LocationId { get; set; }

        public string UserId { get; set; }
    }
}
