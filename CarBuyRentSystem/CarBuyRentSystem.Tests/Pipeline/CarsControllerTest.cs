namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;

    using CarBuyRentSystem.Core.Models.Cars;

    using static Data.Delars;
    using static Data.Cars;
    using static Infrastructure.Data.WebConstants;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Linq;
    using System.Collections.Generic;
    using FluentAssertions;
    using CarBuyRentSystem.Core.Models.View.Cars;
    public class CarsControllerTest
    {
        [Fact]
        public void ShouldReturnViewWhenForValidAllCar()
           => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Cars/All")
                    .WithAntiForgeryToken())
                    .To<CarsController>(c => c.All(AllCarsModel))
                     .Which(controller => controller
                     .WithData(PublicCars))
                   .ShouldReturn()
                   .View(view => view
                    .WithModelOfType<AllCarsViewModel>());

        [Fact]
        public void ShouldReturnViewWhenForValidAllCarTotalCount()
          => MyMvc
               .Pipeline()
               .ShouldMap(request => request
                   .WithPath("/Cars/All")
                   .WithAntiForgeryToken())
                   .To<CarsController>(c => c.All(AllCarsModel))
                    .Which(controller => controller
                    .WithData(PublicCars))
                  .ShouldReturn()
                  .View(view => view
                   .WithModelOfType<AllCarsViewModel>()
                    .Passing(c => c.TotalCars == 15));

        [Fact]
        public void ShouldReturnViewWhenCarIsZeroCount()
         => MyMvc
              .Pipeline()
              .ShouldMap(request => request
                  .WithPath("/Cars/All")
                  .WithAntiForgeryToken())
                  .To<CarsController>(c => c.All(AllCarsModel))
                   .Which(controller => controller
                   .WithData(UserOne))
                 .ShouldReturn()
                 .View(view => view
                  .WithModelOfType<AllCarsViewModel>()
                   .Passing(c => c.TotalCars == 0));

        [Fact]
        public void AddCarForAuthorizedUsersFirstRegistrationForDealer()
        => MyMvc
             .Pipeline()
             .ShouldMap(request => request
                 .WithPath("/Cars/Add")
                  .WithUser()
                 .WithAntiForgeryToken())
                 .To<CarsController>(c => c.Add())
                  .Which(controller => controller
                  .WithData(UserOne))                    
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Create", "Dealers");

        [Fact]
        public void AddCarForAuthorizedUserWhenUserIsDealer()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
                 .WithPath("/Cars/Add")
                  .WithUser()
                 .WithAntiForgeryToken())
                 .To<CarsController>(c => c.Add())
                  .Which(controller => controller
                  .WithData(UserOne)
                  .WithData(SecondDealaer))
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<AddCarFormServiceModel>());

    }
}
