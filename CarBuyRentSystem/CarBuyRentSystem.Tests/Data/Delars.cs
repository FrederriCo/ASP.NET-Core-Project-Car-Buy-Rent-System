using CarBuyRentSystem.Core.Models;
using CarBuyRentSystem.Infrastructure.Models;

namespace CarBuyRentSystem.Tests.Data
{
    public static class Delars
    {
        public static DealerFormServiceModel CreateDealer
           => new DealerFormServiceModel
           {
               Name = "TopAuto",
               PhoneNumber = "+359999888"
           };

        public static DealerFormServiceModel CreateDealerNotValidModel
           => new DealerFormServiceModel
           {
               Name = "Auto Mania",
               PhoneNumber = "1"
           };

        public static Dealer OneDealaer
            => new Dealer
            {
                UserId = "TestUser"
            };
    }
}
