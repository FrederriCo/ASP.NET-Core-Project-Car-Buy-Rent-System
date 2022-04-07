namespace CarBuyRentSystem.Tests.Pipeline
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
    using static Infrastructure.Data.WebConstants;
    public class AdminCarsControllerTest
    {
        [Fact]
        public void AdminAreaIndexShouldReturnView()
          => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/Index")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.Index())
            .Which(controller => controller
              .WithData(PublicCars)
               .WithData(OneDealaer)
                .WithData(UserOne))
            .ShouldReturn()
              .View(view => view
                     .WithModelOfType<TotalUserCar>());

        [Fact]
        public void AdminAreaReturnViewForAllCarsInformation()
        => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/All")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.All())
            .Which(controller => controller
              .WithData(PublicCars)
         .ShouldReturn()
         .View(view => view
                .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()));

        [Fact]
        public void AdminAreadReturnViewForCarsInDataBase()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/All")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.All())
            .Which(controller => controller
              .WithData(PublicCars)
              .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                     .Passing(m => m.Should().HaveCount(15))));

        [Fact]
        public void AdminAreaReturnViewForEditCarsInformation()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/Edit")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.Edit())
             .Which(controller => controller
              .WithData(PublicCars)
              .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()));

        [Fact]
        public void AdminAreaReturnViewWithValidEditCarsInDataBase()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/Edit")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.Edit())
             .Which(controller => controller
              .WithData(PublicCars)
              .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                   .Passing(m => m.Should().HaveCount(15))));

        [Fact]
        public void AdminAreaReturnViewForDeleteCarsInformation()
       => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/Delete")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.Delete())
             .Which(controller => controller
              .WithData(PublicCars)
           .ShouldReturn()
           .View(view => view
                  .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()));

        [Fact]
        public void AdminAreaReturnViewWithValidDeleteCarsInDataBase()
          => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/Delete")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.Delete())
             .Which(controller => controller
              .WithData(PublicCars)
           .ShouldReturn()
           .View(view => view
                  .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                   .Passing(m => m.Should().HaveCount(15))));

        [Fact]
        public void AdminAreaReturnViewWithDataForAllCars()
      => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/AllCars")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.AllCars())
             .Which(controller => controller
              .WithData(PublicCars)
          .ShouldReturn()
          .View(view => view
                 .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                  .Passing(m => m.Should().HaveCount(15))));

        [Fact]
        public void AdminAreaReturnViewWithDataForAllCarsInDataBase()
            => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/AllCars")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.AllCars())
             .Which(controller => controller
              .WithData(PublicCars)
          .ShouldReturn()
          .View(view => view
                 .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()));

        [Fact]
        public void AdminAreaChangeVisabilityAndRediret()
           => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/ChangeVisability/3")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.ChangeVisability(OneCar.Id))
             .Which(controller => controller
              .WithData(OneCar)
               .ShouldReturn()
               .RedirectToAction("All"));

        [Fact]
        public void AdminAreaReturnViewWithRentedCars()
      => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/RentedCars")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.RentedCars())
             .Which(controller => controller
              .WithData(MyRentCars)
          .ShouldReturn()
          .View(view => view
                 .WithModelOfType<IEnumerable<RentedCarsViewModel>>()));

        [Fact]
        public void AdminAreaReturnViewWithValidRentedCarsCount()
           => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/RentedCars")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.RentedCars())
             .Which(controller => controller
              .WithData(MyRentCars)
        .ShouldReturn()
        .View(view => view
               .WithModelOfType<IEnumerable<RentedCarsViewModel>>()
                    .Passing(m => m.Should().HaveCount(1))));

        [Fact]
        public void AdminAreaReturnViewWithSoldCars()
           => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/SoldCars")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.SoldCars())
             .Which(controller => controller
              .WithData(MyBuyCars)
        .ShouldReturn()
        .View(view => view
               .WithModelOfType<IEnumerable<SoldCarsViewModel>>()));

        [Fact]
        public void AdminAreaReturnViewWithSoldCarsCountValid()
            => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Cars/SoldCars")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<CarsController>(x => x.SoldCars())
             .Which(controller => controller
              .WithData(MyBuyCars)
       .ShouldReturn()
       .View(view => view
              .WithModelOfType<IEnumerable<SoldCarsViewModel>>()
               .Passing(m => m.Should().HaveCount(1))));






    }
}
