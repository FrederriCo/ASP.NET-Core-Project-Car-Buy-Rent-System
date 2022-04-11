namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using CarBuyRentSystem.Controllers;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    using static Data.Cars;
    public class BuyCarsControllerTest
    {
        [Fact]
        public void BuyACarForAuthorizedUsersAndReturnView()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/BuyCars/Buy/5")
              .WithUser()
              .WithAntiForgeryToken())
             .To<BuyCarsController>(c => c.Buy(5))
             .Which(controller => controller
              .WithData(PublicCars))
              .ShouldHave()
             .ActionAttributes(attribute => attribute
                 .RestrictingForAuthorizedRequests())
             .AndAlso()
             .ShouldReturn()
           .View(v => v.WithModelOfType<CarServiceListingViewModel>());

        [Fact]
        public void BuyACarForAuthorizedUsersWhenCarIsNoFoundAndReturnApplicationError()
        => MyMvc
            .Pipeline()
            .ShouldMap(request => request
             .WithPath("/BuyCars/Buy/30")
             .WithUser()
             .WithAntiForgeryToken())
            .To<BuyCarsController>(c => c.Buy(30))
            .Which(controller => controller
             .WithData(PublicCars))
             .ShouldHave()
            .ActionAttributes(attribute => attribute
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("ApplicationError", "Home");

        [Fact]
        public void PostBuyCarShuldBeAuthorizedUsersAndReturnApplicatonErrorView()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/BuyCars/Buy")
             .WithMethod(HttpMethod.Post)
              .WithUser()
              .WithAntiForgeryToken())
             .To<BuyCarsController>(c => c.Buy(new BuyCarBindingModel { }))
             .Which(controller => controller
               .WithData(UserSecond))
              .ShouldHave()
             .ActionAttributes(attribute => attribute
                 .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests())
          .AndAlso()
              .ShouldReturn()
              .RedirectToAction("ApplicationError", "Home");

        [Fact]
        public void MyAllBuyCarsShouldReturnView()
          => MyMvc
              .Pipeline()
              .ShouldMap(request => request
               .WithPath("/BuyCars/MyBuyCars")
               .WithUser()
               .WithAntiForgeryToken())
              .To<BuyCarsController>(c => c.MyBuyCars())
              .Which(controller => controller
               .WithData(MyBuyCars))
               .ShouldHave()
              .ActionAttributes(attribute => attribute
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<SoldCarsViewModel>>());

        [Fact]
        public void MyAllBuyCarsShouldReturnViewWhenCountIsZero()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/BuyCars/MyBuyCars")
              .WithUser()
              .WithAntiForgeryToken())
             .To<BuyCarsController>(c => c.MyBuyCars())
             .Which(controller => controller
              .WithData(MyBuyCars))
              .ShouldHave()
             .ActionAttributes(attribute => attribute
                 .RestrictingForAuthorizedRequests())
             .AndAlso()
             .ShouldReturn()
           .View(v => v.WithModelOfType<IEnumerable<SoldCarsViewModel>>()
            .Passing(m => m.Should().HaveCount(0)));
    }
}
