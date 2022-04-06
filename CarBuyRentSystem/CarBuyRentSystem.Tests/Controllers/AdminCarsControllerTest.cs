﻿namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Areas.Admin.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    using static Data.Delars;
    using static Data.Cars;

    public class AdminCarsControllerTest
    {
        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForTotalUserAndCar()
            => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .WithData(UserOne)
                .WithData(OneDealaer)
                .Calling(x => x.Index())
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<TotalUserCar>());

        [Fact]
        public void AdminAreaShouldReturnViewWithDataForAllCarsInformation()
           => MyController<CarsController>
               .Instance()
               .WithData(PublicCars)
               .Calling(x => x.All())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<CarServiceListingViewModel>>());

        [Fact]
        public void AdminAreaShouldReturnViewWithValidCarsInDataBase()
          => MyController<CarsController>
              .Instance()
              .WithData(PublicCars)
              .Calling(x => x.All())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                      .Passing(m => m.Should().HaveCount(15)));

        [Fact]
        public void AdminAreaShouldReturnViewWithDataForEditCarsInformation()
          => MyController<CarsController>
              .Instance()
              .WithData(PublicCars)
              .Calling(x => x.Edit())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>());

        [Fact]
        public void AdminAreaShouldReturnViewWithValidEditCarsInDataBase()
         => MyController<CarsController>
             .Instance()
             .WithData(PublicCars)
             .Calling(x => x.Edit())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                     .Passing(m => m.Should().HaveCount(15)));

        [Fact]
        public void AdminAreaShouldReturnViewWithDataForDeleteCarsInformation()
         => MyController<CarsController>
             .Instance()
             .WithData(PublicCars)
             .Calling(x => x.Delete())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>());

        [Fact]
        public void AdminAreaShouldReturnViewWithValidDeleteCarsInDataBase()
        => MyController<CarsController>
            .Instance()
            .WithData(PublicCars)
            .Calling(x => x.Delete())
            .ShouldReturn()
            .View(view => view
                   .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                    .Passing(m => m.Should().HaveCount(15)));

        [Fact]
        public void AdminAreaShouldReturnViewWithDataForAllCars()
        => MyController<CarsController>
            .Instance()
            .WithData(PublicCars)
            .Calling(x => x.AllCars())
            .ShouldReturn()
            .View(view => view
                   .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                    .Passing(m => m.Should().HaveCount(15)));

        [Fact]
        public void AdminAreaShouldReturnViewWithDataForAllCarsInDataBase()
       => MyController<CarsController>
           .Instance()
           .WithData(PublicCars)
           .Calling(x => x.AllCars())
           .ShouldReturn()
           .View(view => view
                  .WithModelOfType<IEnumerable<CarServiceListingViewModel>>());

        [Fact]
        public void AdminAreaChangeVisabilityAndRediretToAllCars()
            => MyController<CarsController>
                .Instance()
                .WithData(OneCar)
                .Calling(c => c.ChangeVisability(OneCar.Id))
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void AdminAreaShouldReturnViewWithRentedCars()
       => MyController<CarsController>
           .Instance()
           .WithData(MyRentCars)
           .Calling(x => x.RentedCars())
           .ShouldReturn()
           .View(view => view
                  .WithModelOfType<IEnumerable<RentedCarsViewModel>>());

        [Fact]
        public void AdminAreaShouldReturnViewWithValidRentedCarsCount()
     => MyController<CarsController>
         .Instance()
         .WithData(MyRentCars)
         .Calling(x => x.RentedCars())
         .ShouldReturn()
         .View(view => view
                .WithModelOfType<IEnumerable<RentedCarsViewModel>>()
                     .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaShouldReturnViewWithSoldCars()
      => MyController<CarsController>
          .Instance()
          .WithData(MyBuyCars)
          .Calling(x => x.SoldCars())
          .ShouldReturn()
          .View(view => view
                 .WithModelOfType<IEnumerable<SoldCarsViewModel>>());


        [Fact]
        public void AdminAreaShouldReturnViewWithSoldCarsCountValid()
      => MyController<CarsController>
          .Instance()
          .WithData(MyBuyCars)
          .Calling(x => x.SoldCars())
          .ShouldReturn()
          .View(view => view
                 .WithModelOfType<IEnumerable<SoldCarsViewModel>>()
                  .Passing(m => m.Should().HaveCount(1)));


    }
}
