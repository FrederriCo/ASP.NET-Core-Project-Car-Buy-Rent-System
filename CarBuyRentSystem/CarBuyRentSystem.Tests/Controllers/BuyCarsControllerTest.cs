namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    using static Data.Cars;
    using static Infrastructure.Data.WebConstants;
    using FluentAssertions;

    public class BuyCarsControllerTest
    {       
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
                .WithData(PublicCars)
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
                 .WithData(PublicCars)
                .Calling(c => c.Buy(2))
               .ShouldReturn()
               .View(v => v.WithModelOfType<CarServiceListingViewModel>());

        [Fact]
        public void ApplicationErrorWhenCarNodFoundForBuy()
           => MyController<BuyCarsController>
               .Instance()
               .WithUser(c => c.WithIdentifier(UserOne.Id))
               .Calling(x => x.Buy(BuyCarBindig))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                       .RestrictingForHttpMethod(HttpMethod.Post)
                         .RestrictingForAuthorizedRequests())
                .AndAlso()
               .ShouldReturn()
               .RedirectToAction("ApplicationError", "Home");

        [Fact]
        public void BuyACarForAuthorizedUsersWhenCarAndUserIsValidAndHowDaysRent()
           => MyController<BuyCarsController>
               .Instance()
               .WithUser()
               .WithData(UserOne)
               .WithData(OneCar)
               .Calling(c => c.Buy(BuyCarBindig))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
               .TempData(tempData => tempData
                   .ContainingEntryWithValue(SuccessBuyCar))
               .AndAlso()
               .ShouldReturn()
               .RedirectToAction("MyBuyCars", "BuyCars");

        [Fact]
        public void MyAllBuyCarsShouldReturnViewWithCars()
         => MyController<BuyCarsController>
              .Instance()
              .WithUser()
              .WithData(MyBuyCars)
              .Calling(c => c.MyBuyCars())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<SoldCarsViewModel>>());

        [Fact]
        public void MyAllBuyCarsShouldReturnViewWithCarsWhenCountIsZero()
        => MyController<BuyCarsController>
             .Instance()
             .WithUser()
             .Calling(c => c.MyBuyCars())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<SoldCarsViewModel>>()
                     .Passing(m => m.Should().HaveCount(0)));
    }
}
