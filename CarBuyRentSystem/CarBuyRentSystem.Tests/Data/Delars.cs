using CarBuyRentSystem.Core.Models;

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
    }
}
