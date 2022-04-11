namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    using static Data.Cars;    

    public class RentCarsControllerTest
    {
        [Fact]
        public void RentShuldBeAuthorizedUsersAndReturnView()
           => MyMvc
               .Pipeline()
               .ShouldMap(request => request
                .WithPath("/RentCars/Rent/5")
                .WithUser()
                .WithAntiForgeryToken())
               .To<RentCarsController>(c => c.Rent(5))
               .Which(controller => controller
                .WithData(PublicCars))
                .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
             .View(v => v.WithModelOfType<CarServiceListingViewModel>());

        [Fact]
        public void PostRentShuldBeAuthorizedUsersAndReturnApplicatonErrorView()
           => MyMvc
               .Pipeline()
               .ShouldMap(request => request
                .WithPath("/RentCars/Rent")
               .WithMethod(HttpMethod.Post)
                .WithUser()
                .WithAntiForgeryToken())
               .To<RentCarsController>(c => c.Rent(new RentCarBindingModel { }))
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
        public void MyAllRentedCars()
           => MyMvc
               .Pipeline()
               .ShouldMap(request => request
                .WithPath("/RentCars/MyRentCars")
                .WithUser()
                .WithAntiForgeryToken())
               .To<RentCarsController>(c => c.MyRentCars())
               .Which(controller => controller
                .WithData(MyRentCars))
                .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
             .View(v => v.WithModelOfType<IEnumerable<RentedCarsViewModel>>());

        [Fact]
        public void MyAllRentedCarsWhenCountIsZero()
          => MyMvc
              .Pipeline()
              .ShouldMap(request => request
               .WithPath("/RentCars/MyRentCars")
               .WithUser()
               .WithAntiForgeryToken())
              .To<RentCarsController>(c => c.MyRentCars())
              .Which(controller => controller
               .WithData(UserOne))
               .ShouldHave()
              .ActionAttributes(attribute => attribute
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<RentedCarsViewModel>>()
             .Passing(m => m.Should().HaveCount(0)));
    }
}
