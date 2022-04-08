namespace CarBuyRentSystem.Tests.Controllers
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
            => MyController<CarsController>
                    .Instance()
                    .WithData(PublicCars)
                    .Calling(c => c.All(AllCarsModel))
                    .ShouldReturn()
                    .View(view => view
                     .WithModelOfType<AllCarsViewModel>());                        

        [Fact]
        public void ShouldReturnViewWhenForValidAllCarTotalCount()
           => MyController<CarsController>
                   .Instance()
                   .WithData(PublicCars)
                   .Calling(c => c.All(new AllCarsViewModel { }))
                   .ShouldReturn()
                   .View(view => view
                    .WithModelOfType<AllCarsViewModel>()
                    .Passing(c => c.TotalCars == 15));

        [Fact]
        public void ShouldReturnViewWhenForCurrentPage()
          => MyController<CarsController>
                  .Instance()
                  .WithData(PublicCars)
                  .Calling(c => c.All(AllCarsModel))
                  .ShouldReturn()
                  .View(view => view
                   .WithModelOfType<AllCarsViewModel>()
                   .Passing(c => c.CurentPage == 3));

        [Fact]
        public void ShouldReturnViewWhenForValCurrentBrand()
         => MyController<CarsController>
                 .Instance()
                 .WithData(PublicCars)
                 .Calling(c => c.All(AllCarsModel))
                 .ShouldReturn()
                 .View(view => view
                  .WithModelOfType<AllCarsViewModel>()
                  .Passing(c => c.Brand == "Bmw"));

        [Fact]
        public void ShouldReturnViewWhenForValCurrenntSearch()
        => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .Calling(c => c.All(AllCarsModel))
                .ShouldReturn()
                .View(view => view
                 .WithModelOfType<AllCarsViewModel>()
                 .Passing(c => c.Search == "Audi"));

        [Fact]
        public void ShouldReturnViewWhenWhenCarIsZeroCount()
           => MyController<CarsController>
                   .Instance()                   
                   .Calling(c => c.All(AllCarsModel))
                   .ShouldReturn()
                   .View(view => view
                    .WithModelOfType<AllCarsViewModel>()
                    .Passing(c => c.TotalCars == 0));

        [Fact]
        public void AddCarForAuthorizedUsersFirstRegistrationForDealer()
            => MyController<CarsController>
                 .Instance()
                  .WithUser(TestUser.Identifier)
                 .Calling(c => c.Add())
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests())
                 .AndAlso()
                 .ShouldReturn()
                .RedirectToAction("Create", "Dealers");

        [Fact]
        public void AddCarForAuthorizedUserWhenUserIsDealer()
               => MyController<CarsController>
                   .Instance()
                    .WithData(SecondDealaer)
                    .WithUser(TestUser.Identifier)
                   .Calling(c => c.Add())
                   .ShouldHave()
                   .ActionAttributes(attributes => attributes
                       .RestrictingForAuthorizedRequests())
                   .AndAlso()
                   .ShouldReturn()
                   .View(view => view
                       .WithModelOfType<AddCarFormServiceModel>());

        [Fact]
        public void PostAddCarForAuthorizedUsersFirstRegistrationForDealer()
                => MyController<CarsController>
                    .Instance()
                     .WithUser(TestUser.Identifier)
                    .Calling(c => c.Add(AddCarService))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(HttpMethod.Post))
                    .AndAlso()
                    .ShouldReturn()
                    .RedirectToAction("Create", "Dealers");

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelState()
              => MyController<CarsController>
                  .Instance()
                   .WithData(SecondDealaer)
                   .WithUser(TestUser.Identifier)
                  .Calling(c => c.Add(NotValidModelAddCar))
                  .ShouldHave()
                  .ActionAttributes(attributes => attributes
                      .RestrictingForHttpMethod(HttpMethod.Post)
                      .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldHave()
                    .ModelState(modelstate => modelstate
                        .For<AddCarFormServiceModel>()
                        .ContainingNoErrorFor(c => c.Model)
                        .AndAlso()
                        .ContainingErrorFor(c => c.Brand)
                        .ThatEquals(ErrorMessagesCarAddBrandModel))
                     .AndAlso()
                     .ShouldReturn()
                     .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateDescription()
             => MyController<CarsController>
                 .Instance()
                  .WithData(SecondDealaer)
                  .WithUser(TestUser.Identifier)
                 .Calling(c => c.Add(NotValidModelAddCar))
                 .ShouldHave()
                 .ActionAttributes(attributes => attributes
                     .RestrictingForHttpMethod(HttpMethod.Post)
                     .RestrictingForAuthorizedRequests())
                   .AndAlso()
                   .ShouldHave()
                   .ModelState(modelstate => modelstate
                       .For<AddCarFormServiceModel>()
                       .ContainingNoErrorFor(c => c.Model)
                       .AndAlso()
                       .ContainingErrorFor(c => c.Description)
                       .ThatEquals(ErrorMessagesCarAddDescription))
                    .AndAlso()
                    .ShouldReturn()
                    .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModel()
            => MyController<CarsController>
                .Instance()
                 .WithData(SecondDealaer)
                 .WithUser(TestUser.Identifier)
                .Calling(c => c.Add(NotValidModelAddCarOther))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                  .AndAlso()
                  .ShouldHave()
                  .ModelState(modelstate => modelstate
                      .For<AddCarFormServiceModel>()
                      .ContainingNoErrorFor(c => c.Brand)
                      .AndAlso()
                      .ContainingErrorFor(c => c.ImageUrl)
                      .ThatEquals(ErrorMessagesCarAddImageUrl))
                   .AndAlso()
                   .ShouldReturn()
                   .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelForInvalidYear()
           => MyController<CarsController>
               .Instance()
                .WithData(SecondDealaer)
                .WithUser(TestUser.Identifier)
               .Calling(c => c.Add(NotValidModelAddCarOther))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
                 .AndAlso()
                 .ShouldHave()
                 .ModelState(modelstate => modelstate
                     .For<AddCarFormServiceModel>()
                     .ContainingNoErrorFor(c => c.Brand)
                     .AndAlso()
                     .ContainingErrorFor(c => c.Year)
                     .ThatEquals(ErrorMessagesCarAddInvalidYear))
                  .AndAlso()
                  .ShouldReturn()
                  .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelForInvalidDoors()
          => MyController<CarsController>
              .Instance()
               .WithData(SecondDealaer)
               .WithUser(TestUser.Identifier)
              .Calling(c => c.Add(NotValidModelAddCarOther))
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ModelState(modelstate => modelstate
                    .For<AddCarFormServiceModel>()
                    .ContainingNoErrorFor(c => c.Brand)
                    .AndAlso()
                    .ContainingErrorFor(c => c.Doors)
                    .ThatEquals(ErrorMessagesCarAddInvalidDoors))
                 .AndAlso()
                 .ShouldReturn()
                 .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelForInvalidPrice()
          => MyController<CarsController>
              .Instance()
               .WithData(SecondDealaer)
               .WithUser(TestUser.Identifier)
              .Calling(c => c.Add(NotValidModelAddCarOther))
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ModelState(modelstate => modelstate
                    .For<AddCarFormServiceModel>()
                    .ContainingNoErrorFor(c => c.Brand)
                    .AndAlso()
                    .ContainingErrorFor(c => c.Price)
                    .ThatEquals(ErrorMessagesCarAddInvalidPrice))
                 .AndAlso()
                 .ShouldReturn()
                 .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelForInvalidPassager()
         => MyController<CarsController>
             .Instance()
              .WithData(SecondDealaer)
              .WithUser(TestUser.Identifier)
             .Calling(c => c.Add(NotValidModelAddCarOther))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForHttpMethod(HttpMethod.Post)
                 .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldHave()
               .ModelState(modelstate => modelstate
                   .For<AddCarFormServiceModel>()
                   .ContainingNoErrorFor(c => c.Brand)
                   .AndAlso()
                   .ContainingErrorFor(c => c.Passager)
                   .ThatEquals(ErrorMessagesCarAddInvalidPassager))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelForInvalidPricePeerDay()
         => MyController<CarsController>
             .Instance()
              .WithData(SecondDealaer)
              .WithUser(TestUser.Identifier)
             .Calling(c => c.Add(NotValidModelAddCarOther))
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForHttpMethod(HttpMethod.Post)
                 .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldHave()
               .ModelState(modelstate => modelstate
                   .For<AddCarFormServiceModel>()
                   .ContainingNoErrorFor(c => c.Brand)
                   .AndAlso()
                   .ContainingErrorFor(c => c.RentPricePerDay)
                   .ThatEquals(ErrorMessagesCarAddInvalidPrice))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelForInvalidLugage()
        => MyController<CarsController>
            .Instance()
             .WithData(SecondDealaer)
             .WithUser(TestUser.Identifier)
            .Calling(c => c.Add(NotValidModelAddCarOther))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldHave()
              .ModelState(modelstate => modelstate
                  .For<AddCarFormServiceModel>()
                  .ContainingNoErrorFor(c => c.Description)
                  .AndAlso()
                  .ContainingErrorFor(c => c.Lugage)
                  .ThatEquals(ErrorMessagesCarAddInvalidLugage))
               .AndAlso()
               .ShouldReturn()
               .View();



        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealrWhenLocationDoeseNotExists()
            => MyController<CarsController>
                .Instance()
                 .WithData(SecondDealaer)
                 .WithUser(TestUser.Identifier)
                .Calling(c => c.Add(AddCarService))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View();


        [Fact]
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealer()
            => MyController<CarsController>
                .Instance()
                  .WithData(LocationAdd)
                 .WithData(SecondDealaer)
                 .WithUser(TestUser.Identifier)
                .Calling(c => c.Add(AddCarService))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
            .TempData(tempData => tempData
                .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("DealerCar");

        [Fact]
        public void GetDealerAllCarForAuthorizedUsersWhenCarsIsZero()
            => MyController<CarsController>
                .Instance()
                  .WithData(LocationAdd)
                 .WithData(SecondDealaer)
                 .WithUser(TestUser.Identifier)
                  .Calling(c => c.DealerCar())
               .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                        .Passing(model => model.Should().HaveCount(0)));

        [Fact]
        public void GetDealerAllCarForAuthorizedUsers()
           => MyController<CarsController>
               .Instance()
                 .WithData(LocationAdd)
                .WithData(SecondDealaer)
                .WithData(OneCar)
                .WithUser(TestUser.Identifier)
                 .Calling(c => c.DealerCar())
              .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View(view => view
                    .WithModelOfType<IEnumerable<CarServiceListingViewModel>>()
                       .Passing(model => model.Should().HaveCount(1)));

        [Fact]
        public void EditCarForAuthorizedUsersWhenUserIsNotADealer()
          => MyController<CarsController>
              .Instance()
                .WithData(OneCar)
               .WithUser(TestUser.Identifier)
                .Calling(c => c.Edit(OneCar.Id))
             .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .RedirectToAction("Create");

        [Fact]
        public void EditCarForAuthorizedUsersWhenUserIsIsNotValid()
          => MyController<CarsController>
              .Instance()
                .WithData(OneCar)
                .WithData(SecondDealaer)
                .WithData(LocationAdd)
               .WithUser(TestUser.Identifier)
                .Calling(c => c.Edit(OneCar.Id))
             .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
                .ShouldReturn()
                .BadRequest();

        [Fact]
        public void PostEditCarForAuthorizedUsersWhenUserIsDealer()
         => MyController<CarsController>
             .Instance()
               .WithData(OneCar)
               .WithData(SecondDealaer)
               .WithData(LocationAdd)
              .WithUser(TestUser.Identifier)
               .Calling(c => c.Edit(CarEdit))
            .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
               .ShouldHave()
            .TempData(tempData => tempData
                .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("DealerCar");

        [Fact]
        public void PostEditCarForAuthorizedUsersWhenUserIsNotADealer()
     => MyController<CarsController>
         .Instance()
           .WithData(OneCar)
           .WithUser(TestUser.Identifier)
           .Calling(c => c.Edit(CarEdit))
        .ShouldHave()
         .ActionAttributes(attributes => attributes
             .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .AndAlso()
        .ShouldReturn()
        .RedirectToAction("Create", "Dealers");

        [Fact]
        public void PostEditCarForAuthorizedUsersWhenUserADealerWhenLocationDoesNotExists()
   => MyController<CarsController>
       .Instance()
         .WithData(OneCar)
          .WithData(SecondDealaer)
         .WithUser(TestUser.Identifier)
         .Calling(c => c.Edit(CarEdit))
      .ShouldHave()
       .ActionAttributes(attributes => attributes
           .RestrictingForHttpMethod(HttpMethod.Post)
              .RestrictingForAuthorizedRequests())
          .AndAlso()
      .ShouldReturn()
      .View();

        [Fact]
        public void PostEditCarForAuthorizedUsersWhenUserCarIsNotADealerId()
        => MyController<CarsController>
            .Instance()
              .WithData(SecondCar)
              .WithData(SecondDealaer)
              .WithData(LocationAdd)
             .WithUser(TestUser.Identifier)
              .Calling(c => c.Edit(CarEdit))
           .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
              .ShouldReturn()
              .BadRequest();

        [Fact]
        public void ShouldReturnApplicationErrorWhenCarNotFoundForDelete()
          => MyController<CarsController>
              .Instance()
              .WithData(OneCar)
              .Calling(x => x.Delete(1))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("ApplicationError", "Home");

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDelete()
        => MyController<CarsController>
            .Instance()
            .WithUser()
            .WithData(OneCar)
            .Calling(x => x.Delete(OneCar.Id))
           .ShouldHave()
             .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
               .ShouldHave()
            .TempData(tempData => tempData
                .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("DealerCar");

        [Fact]
        public void ShouldReturnAdminAreaViewWhenUserIsAdmin()
       => MyController<CarsController>
           .Instance()
           .WithUser(x => x.InRole(AdministratorRoleName))
           .WithData(OneCar)
           .Calling(x => x.Delete(OneCar.Id))
          .ShouldHave()
            .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
            .ShouldReturn()
           .RedirectToAction("All", "Cars", new { area = "Admin" });

        [Fact]
        public void ShouldReturnBadRequestWhenCarNotFoundForDetails()
         => MyController<CarsController>
             .Instance()
             .WithData(OneCar)
             .Calling(x => x.Details(5))
           .ShouldReturn()
           .BadRequest();

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetails()
         => MyController<CarsController>
             .Instance()
             .WithData(OneCar)
             .Calling(x => x.Details(OneCar.Id))
           .ShouldReturn()
           .View(view => view
                     .WithModelOfType<CarServiceListingViewModel>());
    }
}
