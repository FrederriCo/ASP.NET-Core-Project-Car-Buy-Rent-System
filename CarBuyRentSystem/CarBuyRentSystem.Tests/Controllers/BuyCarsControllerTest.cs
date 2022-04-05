namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.View.Cars;

    using static Data.Cars;
    using CarBuyRentSystem.Core.Models.Cars;

    public class BuyCarsControllerTest
    {
        [Fact]
        public void PostBuyShuldBeMapped()
          => MyRouting
              .Configuration()
                .ShouldMap(request => request
               .WithPath("/BuyCars/Buy/32")
                .WithMethod(HttpMethod.Post))
              .To<BuyCarsController>(c => c.Buy(32));

        [Fact]
        public void BuyACarForAuthorizedUsers()
             => MyController<BuyCarsController>
                 .Instance()
                  .WithUser(TestUser.Identifier)
                 .Calling(c => c.Buy(32))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());

        [Fact]
        public void BuyACarForAuthorizedUsersWhenCarIsNoFoundInDataBase()
           => MyController<BuyCarsController>
               .Instance()
               .WithUser(TestUser.Identifier)
                .WithData(TenPublicCars())
               .Calling(c => c.Buy(20))
                .ShouldHave()
               .ActionAttributes(attributes => attributes
                       .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ApplicationError", "Home");

        [Fact]
        public void BuyACarForAuthorizedUsersWhenCarIsValidInDataBase()
          => MyController<BuyCarsController>
               .Instance()
                 .WithUser(TestUser.Identifier)
                 .WithData(TenPublicCars())
                .Calling(c => c.Buy(2))
               .ShouldReturn()
               .View(v => v.WithModelOfType<CarServiceListingViewModel>());
    }
}
