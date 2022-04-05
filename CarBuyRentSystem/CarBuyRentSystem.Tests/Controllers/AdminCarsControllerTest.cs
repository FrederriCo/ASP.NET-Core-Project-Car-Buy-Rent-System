namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Areas.Admin.Controllers;

    using CarBuyRentSystem.Core.Models.Cars;

    using static Data.Delars;
    using static Data.Cars;
    using FluentAssertions;
    using System.Collections.Generic;

    public class AdminCarsControllerTest
    {
        [Fact]
        public void GetAllShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/Index")
               .To<CarsController>(c => c.Index());

        [Fact]
        public void GetMineShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Cars/ChangeVisability/1")
                .To<CarsController>(c => c.ChangeVisability(1));

        [Fact]
        public void GetAllCarsShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/AllCars")
               .To<CarsController>(c => c.AllCars());

        [Fact]
        public void GetAllInformationShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/All")
               .To<CarsController>(c => c.All());

        [Fact]
        public void GetEditShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/Edit")
               .To<CarsController>(c => c.Edit());

        [Fact]
        public void GetDeleteShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/Delete")
               .To<CarsController>(c => c.Delete());

        [Fact]
        public void GetRentedCarsShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Admin/Cars/RentedCars")
              .To<CarsController>(c => c.RentedCars());

        [Fact]
        public void GetSoldCarsCarsShouldBeRoutedCorrectly()
         => MyRouting
             .Configuration()
             .ShouldMap("/Admin/Cars/SoldCars")
             .To<CarsController>(c => c.SoldCars());


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
             .Calling(x => x.All())
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





    }
}
