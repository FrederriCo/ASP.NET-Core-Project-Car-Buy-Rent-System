namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;

    using static Data.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;
    using static Infrastructure.Data.WebConstants;
    using System.Collections.Generic;

    public class RentCarsControllerTest
    {
        [Fact]
        public void CreateShuldBeAuthorizedUsersAndReturnView()
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
        public void PostCreateShuldBeAuthorizedUsersAndReturnApplicatonErrorView()
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

        //[Fact]
        //public void PostCreateShuldBeAuthorizedUsersAndReturnView()
        //  => MyMvc
        //      .Pipeline()
        //      .ShouldMap(request => request
        //       .WithPath("/RentCars/Rent")
        //      .WithMethod(HttpMethod.Post)
        //       .WithUser()
        //       .WithAntiForgeryToken())
        //      .To<RentCarsController>(c => c.Rent(new RentCarBindingModel { }))
        //      .Which(controller => controller
        //        .WithData(UserSecond)
        //        .WithData(SecondCar))
        //       .ShouldHave()
        //      .ActionAttributes(attribute => attribute
        //          .RestrictingForHttpMethod(HttpMethod.Post)
        //           .RestrictingForAuthorizedRequests())
        //        .TempData(tempData => tempData
        //            .ContainingEntryWithValue(SuccessRentCar))
        //        .AndAlso()
        //        .ShouldReturn()
        //        .RedirectToAction("MyRentCars", "RentCars");

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

    }
}
