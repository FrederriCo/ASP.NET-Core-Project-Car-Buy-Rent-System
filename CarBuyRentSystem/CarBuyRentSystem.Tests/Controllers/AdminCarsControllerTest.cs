namespace CarBuyRentSystem.Tests.Controllers
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
        public void AdminAreaIndexShouldReturnViewWitSoldCarZeroCount()
           => MyController<CarsController>
               .Instance()
               .WithData(UserOne)
               .Calling(x => x.Index())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<TotalUserCar>()
                       .Passing(model => model.TotalSoldCars == 0));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWitSoldCars()
           => MyController<CarsController>
               .Instance()
               .WithData(UserOne)
                .WithData(MyBuyCars)
               .Calling(x => x.Index())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<TotalUserCar>()
                       .Passing(model => model.TotalSoldCars == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewForRentCarsZeroCount()
          => MyController<CarsController>
              .Instance()
              .WithData(UserOne)
              .Calling(x => x.Index())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<TotalUserCar>()
                      .Passing(model => model.TotalRentCars == 0));

        [Fact]
        public void AdminAreaIndexShouldReturnViewForRentCars()
          => MyController<CarsController>
             .Instance()
             .WithData(UserSecond)
              .WithData(MyRentCars)
             .Calling(x => x.Index())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<TotalUserCar>()
                     .Passing(model => model.TotalRentCars == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForOneUser()
            => MyController<CarsController>
                .Instance()
                .WithData(UserOne)
                .Calling(x => x.Index())
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<TotalUserCar>()
                        .Passing(model => model.TotalUser == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithNoUsers()
           => MyController<CarsController>
               .Instance()
               .Calling(x => x.Index())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<TotalUserCar>()
                       .Passing(model => model.TotalUser == 0));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWitNoDealer()
          => MyController<CarsController>
              .Instance()
              .Calling(x => x.Index())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<TotalUserCar>()
                      .Passing(model => model.TotalDealer == 0));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForOneDealer()
           => MyController<CarsController>
               .Instance()
               .WithData(OneDealaer)
               .Calling(x => x.Index())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<TotalUserCar>()
                       .Passing(model => model.TotalDealer == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithNoCars()
        => MyController<CarsController>
            .Instance()
            .Calling(x => x.Index())
            .ShouldReturn()
            .View(view => view
                   .WithModelOfType<TotalUserCar>()
                    .Passing(model => model.TotalCar == 0));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllCars()
          => MyController<CarsController>
              .Instance()
              .WithData(PublicCars)
              .Calling(x => x.Index())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<TotalUserCar>()
                      .Passing(model => model.TotalCar == 15));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForOneCar()
         => MyController<CarsController>
             .Instance()
             .WithData(OneCar)
             .Calling(x => x.Index())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<TotalUserCar>()
                     .Passing(model => model.TotalCar == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForUsersDealersAndCarsWhenIsZero()
        => MyController<CarsController>
               .Instance()
               .Calling(x => x.Index())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<TotalUserCar>()
                       .Passing(model => model.TotalUser == 0 &&
                                 model.TotalDealer == 0 &&
                                  model.TotalCar == 0 &&
                                  model.TotalSoldCars == 0 &&
                                model.TotalRentCars == 0));
        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForUsersDealersAndCars()
         => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .WithData(UserSecond)
                .WithData(OneDealaer)
                .Calling(x => x.Index())
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<TotalUserCar>()
                        .Passing(model => model.TotalUser == 1 &&
                                  model.TotalDealer == 1 &&
                                   model.TotalCar == 15));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForSoldCarsAndRentCars()
        => MyController<CarsController>
               .Instance()               
               .WithData(SecondMyRentCars)
               .WithData(SecondMyBuyCars)
               .Calling(x => x.Index())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<TotalUserCar>()
                       .Passing(model => model.TotalRentCars == 1 &&
                                  model.TotalSoldCars == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForUsersDealersAndCarsRentCars()
       => MyController<CarsController>
              .Instance()
              .WithData(UserOne)
              .WithData(OneDealaer)
              .WithData(SecondMyRentCars)
              .Calling(x => x.Index())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<TotalUserCar>()
                      .Passing(model => model.TotalUser == 1 &&
                                model.TotalDealer == 1 &&
                                model.TotalRentCars == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForUsersDealersAndCarsSoldCars()
            => MyController<CarsController>
                 .Instance()
                 .WithData(UserOne)
                 .WithData(SecondMyRentCars)
                 .WithData(SecondMyBuyCars)
                 .Calling(x => x.Index())
                 .ShouldReturn()
                 .View(view => view
                        .WithModelOfType<TotalUserCar>()
                         .Passing(model => model.TotalUser == 1 &&
                                       model.TotalRentCars == 1 &&
                                   model.TotalSoldCars == 1));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithAllData()
          => MyController<CarsController>
               .Instance()
               .WithData(PublicCars)
               .WithData(UserOne)
               .WithData(OneDealaer)
                .WithData(SecondMyRentCars)
                .WithData(SecondMyBuyCars)
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
        public void AdminAreaShouldReturnViewWithValidCars()
          => MyController<CarsController>
              .Instance()
              .WithData(PublicCars)
              .Calling(x => x.All())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                      .Passing(m => m.Should().HaveCount(15)));

        [Fact]
        public void AdminAreaShouldReturnViewWithCarIsZeroCount()
         => MyController<CarsController>
             .Instance()
             .Calling(x => x.All())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                     .Passing(m => m.Should().HaveCount(0)));

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
        public void AdminAreaShouldReturnViewWithCarsForEditIsZero()
         => MyController<CarsController>
             .Instance()
             .Calling(x => x.Edit())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                     .Passing(m => m.Should().HaveCount(0)));

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
        public void AdminAreaShouldReturnViewWithCarForDeleteIsZero()
             => MyController<CarsController>
                 .Instance()
                 .Calling(x => x.Delete())
                 .ShouldReturn()
                 .View(view => view
                        .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                         .Passing(m => m.Should().HaveCount(0)));
               
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
        public void AdminAreaShouldReturnViewWithDataCarsIsZeroCount()
             => MyController<CarsController>
                .Instance()
                .Calling(x => x.AllCars())
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                        .Passing(m => m.Should().HaveCount(0)));

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
        public void AdminAreaShouldReturnViewWithValidRentedCarsCountIsZero()
          => MyController<CarsController>
             .Instance()
             .Calling(x => x.RentedCars())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<RentedCarsViewModel>>()
                         .Passing(m => m.Should().HaveCount(0)));
           
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
        public void AdminAreaShouldReturnViewWithSoldCarsCarsIsZero()
           => MyController<CarsController>
                .Instance()
                .Calling(x => x.SoldCars())
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<IEnumerable<SoldCarsViewModel>>()
                .Passing(m => m.Should().HaveCount(0)));

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
