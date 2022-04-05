namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    using static Infrastructure.Data.WebConstants;
    using static Data.Cars;

    public class RentCarsControllerTest
    {
        [Fact]
        public void PostRentShuldBeMapped()
          => MyRouting
              .Configuration()
                .ShouldMap(request => request
               .WithPath("/RentCars/Rent/32")
                .WithMethod(HttpMethod.Post))
              .To<RentCarsController>(c => c.Rent(32));


        [Fact]
        public void RentACarForAuthorizedUsers()
               => MyController<RentCarsController>
                   .Instance()
                    .WithUser(TestUser.Identifier)
                   .Calling(c => c.Rent(32))
                   .ShouldHave()
                   .ActionAttributes(attributes => attributes
                       .RestrictingForAuthorizedRequests());


        [Fact]
        public void RentACarForAuthorizedUsersWhenCarIsNoFoundInDataBase()
           => MyController<RentCarsController>
               .Instance()
               .WithUser(TestUser.Identifier)
                .WithData(PublicCars)
               .Calling(c => c.Rent(20))
                .ShouldHave()
               .ActionAttributes(attributes => attributes
                       .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ApplicationError", "Home");


        [Fact]
        public void RentACarForAuthorizedUsersWhenCarIsValidInDataBase()
           => MyController<RentCarsController>
                .Instance()
                  .WithUser(TestUser.Identifier)
                  .WithData(PublicCars)
                 .Calling(c => c.Rent(2))
                .ShouldReturn()
                .View(v => v.WithModelOfType<CarServiceListingViewModel>());


        [Fact]
        public void ApplicationErrorWhenCarNodFoundForRent()
            => MyController<RentCarsController>
                .Instance()
                .WithUser(c => c.WithIdentifier(UserOne.Id))
                .Calling(x => x.Rent(RentCarBindig))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                       .RestrictingForHttpMethod(HttpMethod.Post)
                       .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ApplicationError", "Home");


        [Fact]
        public void RentACarForAuthorizedUsersWhenCarAndUserIsValidAndHowDaysRent()
            => MyController<RentCarsController>
                .Instance()
                .WithUser()
                .WithData(UserOne)
                .WithData(OneCar)
                .Calling(c => c.Rent(RentCarBindig))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .TempData(tempData => tempData
                    .ContainingEntryWithValue(SuccessRentCar))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyRentCars", "RentCars");


        [Fact]
        public void MyAllRentedCarsRoutShouldBeMapeer()
            => MyRouting
              .Configuration()
                .ShouldMap(request => request
               .WithPath("/RentCars/MyRentCars")
                .WithUser()
                .WithMethod(HttpMethod.Post))
              .To<RentCarsController>(c => c.MyRentCars());


        [Fact]
        public void MyAllRentedCarsShouldReturnViewWithCars()
           => MyController<RentCarsController>
                .Instance()
                .WithUser()
                .WithData(MyRentCars)
                .Calling(c => c.MyRentCars())
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<IEnumerable<RentedCarsViewModel>>());

    }
}
