namespace CarBuyRentSystem.Tests.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.View.Cars;

    public class BuyCarsControllerTest
    {
        [Fact]
        public void BuyShuldBeMapped()
         => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/BuyCars/Buy/32")
               .WithUser())               
             .To<BuyCarsController>(c => c.Buy(32));

        [Fact]
        public void PostBuyShuldBeMapped()
         => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/BuyCars/Buy/32")
               .WithUser()
               .WithMethod(HttpMethod.Post))
             .To<BuyCarsController>(c => c.Buy(32));

        [Fact]
        public void PostBuyCarsRoutShouldBeMapeer()
          => MyRouting
            .Configuration()
              .ShouldMap(request => request
             .WithPath("/BuyCars/Buy")
              .WithUser()
              .WithMethod(HttpMethod.Post))
            .To<BuyCarsController>(c => c.Buy(With.Any<BuyCarBindingModel>()));

        [Fact]
        public void MyAllBuyCarsRoutShouldBeMapeer()
           => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/BuyCars/MyBuyCars")
               .WithUser()
               .WithMethod(HttpMethod.Post))
             .To<BuyCarsController>(c => c.MyBuyCars());
    }
}
