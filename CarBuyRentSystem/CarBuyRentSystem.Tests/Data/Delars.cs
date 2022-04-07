using CarBuyRentSystem.Core.Models;
using CarBuyRentSystem.Infrastructure.Models;

namespace CarBuyRentSystem.Tests.Data
{
    public static class Delars
    {
        public static string ErrorMeesagessDealer = "The field Phone Number must be a string with a minimum length of 4 and a maximum length of 20.";

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
                Id = 4,
                UserId = "TestUser"
            };

        public static Dealer SecondDealaer
            => new Dealer
            {
                Id = 4,
                UserId = "TestId"
            };
    }
}
