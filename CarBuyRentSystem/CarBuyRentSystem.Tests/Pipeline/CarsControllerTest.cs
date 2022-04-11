namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars.Enums;

    using static Data.Delars;
    using static Data.Cars;
    using static Infrastructure.Data.WebConstants;

    public class CarsControllerTest
    {
        [Fact]
        public void ShouldReturnViewForMyWallet()
           => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Cars/MyWallet")
                    .WithUser()
                    .WithAntiForgeryToken())
                    .To<CarsController>(c => c.MyWallet())
                     .Which(controller => controller
                     .WithData(UserOne))
                    .ShouldHave()
                   .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests())                    
                    .AndAlso()
                   .ShouldReturn()
                   .View();
       
        [Fact]
        public void ShouldReturnViewWhenForValidAllCar()
           => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Cars/All")
                    .WithAntiForgeryToken())
                    .To<CarsController>(c => c.All(new AllCarsViewModel { }))
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
                   .To<CarsController>(c => c.All(new AllCarsViewModel { }))
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
                  .To<CarsController>(c => c.All(new AllCarsViewModel { }))
                   .Which(controller => controller
                   .WithData(UserOne))
                 .ShouldReturn()
                 .View(view => view
                  .WithModelOfType<AllCarsViewModel>()
                   .Passing(c => c.TotalCars == 0));

        [Fact]
        public void ShouldReturnViewWhenBrandsIsZeroCount()
       => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/All")
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.All(new AllCarsViewModel { }))
                 .Which(controller => controller
                 .WithData(UserOne))
               .ShouldReturn()
               .View(view => view
                .WithModelOfType<AllCarsViewModel>()
                 .Passing(c => c.Brands.Should().HaveCount(0)));

        [Fact]
        public void ShouldReturnViewForSortingDateCreated()
      => MyMvc
           .Pipeline()
           .ShouldMap(request => request
               .WithPath("/Cars/All")
               .WithAntiForgeryToken())
               .To<CarsController>(c => c.All(new AllCarsViewModel { Sorting = CarSorting.DateCreated }))
                .Which(controller => controller
                .WithData(UserOne))
              .ShouldReturn()
              .View(view => view
               .WithModelOfType<AllCarsViewModel>()
                .Passing(c => c.Sorting == CarSorting.DateCreated));

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

        [Fact]
        public void PostAddCarForAuthorizedUsersFirstRegistrationForDealer()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
                 .WithPath("/Cars/Add")
                  .WithUser()
                    .WithMethod(HttpMethod.Post)
                 .WithAntiForgeryToken())
                 .To<CarsController>(c => c.Add(new AddCarFormServiceModel { }))
                  .Which(controller => controller
                  .WithData(OneCar)
                  .WithData(UserOne))
                  .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Create", "Dealers");

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealrWhenLocationDoeseNotExists()
        => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Add")
                 .WithUser()
                   .WithMethod(HttpMethod.Post)
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Add(new AddCarFormServiceModel { }))
                 .Which(controller => controller
                 .WithData(SecondDealaer))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests()
               .RestrictingForHttpMethod(HttpMethod.Post))
               .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetDealerAllValidCarForAuthorizedUsers()
         => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/DealerCars")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.DealerCars())
                 .Which(controller => controller
                 .WithData(SecondDealaer)
                 .WithData(LocationAdd)
                 .WithData(OneCar))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>());

        [Fact]
        public void GetDealerCarForAuthorizedUsersWhenCarsIsZero()
         => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/DealerCars")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.DealerCars())
                 .Which(controller => controller
                 .WithData(SecondDealaer)
                 .WithData(LocationAdd))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                            .Passing(model => model.Should().HaveCount(0)));


        [Fact]
        public void EditCarForAuthorizedUsersWhenUserIsNotADealer()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Edit/3")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Edit(OneCar.Id))
                 .Which(controller => controller
                 .WithData(OneCar))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Create");

        [Fact]
        public void EditCarForAuthorizedUsersWhenUserIsIsNotValid()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Edit/3")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Edit(OneCar.Id))
                 .Which(controller => controller
                 .WithData(OneCar)
                 .WithData(SecondDealaer)
                 .WithData(LocationAdd))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                 .BadRequest();

        [Fact]
        public void ShouldReturnApplicationErrorWhenCarNotFoundForDelete()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Delete/1")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Delete(1))
                 .Which(controller => controller
                 .WithData(OneCar))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ApplicationError", "Home");

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDelete()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Delete/3")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Delete(OneCar.Id))
                 .Which(controller => controller
                 .WithData(OneCar))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .RedirectToAction("DealerCars");

        [Fact]
        public void ShouldReturnAdminAreaViewWhenUserIsAdmin()
             => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Delete/3")
                 .WithUser(x => x.InRole(AdministratorRoleName))
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Delete(OneCar.Id))
                 .Which(controller => controller
                 .WithData(OneCar))
                 .ShouldHave()
               .ActionAttributes(attribute => attribute
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All", "Cars", new { area = "Admin" });

        [Fact]
        public void ShouldReturnBadRequestWhenCarNotFoundForDetails()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Details/5")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Details(5))
                 .Which(controller => controller
                 .WithData(OneCar))
                 .ShouldReturn()
                 .BadRequest();

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetails()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Cars/Details/3")
                 .WithUser()
                .WithAntiForgeryToken())
                .To<CarsController>(c => c.Details(OneCar.Id))
                 .Which(controller => controller
                 .WithData(OneCar))
                 .ShouldReturn()
                .View(view => view
                     .WithModelOfType<CarServiceListingViewModel>());
    }
}
