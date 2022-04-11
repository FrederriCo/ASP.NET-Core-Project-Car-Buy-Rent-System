namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using FluentAssertions;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using CarBuyRentSystem.Core.Models.View.Cars.Enums;

    using static Data.Delars;
    using static Data.Cars;
    using static Infrastructure.Data.WebConstants;

    public class CarsControllerTest
    {
        [Fact]
        public void ShouldReturnViewForMyWallet()
           => MyController<CarsController>
                   .Instance()
                    .WithUser()
                   .Calling(c => c.MyWallet())
                   .ShouldReturn()
                   .View();

        [Fact]
        public void PostShouldReturnViewForMyWalletInvalidModelState()
           => MyController<CarsController>
                   .Instance()
                    .WithUser()
                   .Calling(c => c.MyWallet(new AddMyWalletBindingModel { Balance = -1, UserId = TestUser.Identifier }))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests())
                      .AndAlso()
                   .ShouldReturn()
                   .View();

        [Fact]
        public void PostShouldReturnViewForMyWalletInvalidModelStateErrorMessage()
              => MyController<CarsController>
                  .Instance()
                   .WithData(UserOne)
                   .WithUser(TestUser.Identifier)
                  .Calling(c => c.MyWallet(NegativeBalance))
                  .ShouldHave()
                  .ActionAttributes(attributes => attributes
                      .RestrictingForHttpMethod(HttpMethod.Post)
                      .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldHave()
                    .ModelState(modelstate => modelstate
                        .For<AddMyWalletBindingModel>()
                        .ContainingNoErrorFor(c => c.UserId)
                        .AndAlso()
                        .ContainingErrorFor(c => c.Balance)
                        .ThatEquals(ErrorMessagesBalance))
                     .AndAlso()
                     .ShouldReturn()
                     .View();

        [Fact]
        public void PostShouldReturnViewForMyWallet()
          => MyController<CarsController>
                  .Instance()
                    .WithUser()
                   .WithData(UserOne)
                  .Calling(c => c.MyWallet(new AddMyWalletBindingModel { Balance = 200, UserId = UserOne.Id}))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests()
                     .RestrictingForHttpMethod(HttpMethod.Post))
                    .AndAlso()
                  .ShouldReturn()
                  .RedirectToAction("MyWallet", "Cars");

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
        public void ShouldReturnViewWithoutCar()
           => MyController<CarsController>
                   .Instance()
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
        public void ShouldReturnViewWhenForCurrentIsPageZero()
        => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .Calling(c => c.All(new AllCarsViewModel { CurentPage = 0 }))
                .ShouldReturn()
                .View(view => view
                 .WithModelOfType<AllCarsViewModel>()
                 .Passing(c => c.CurentPage == 0));

        [Fact]
        public void ShouldReturnViewWhenForValidCurrentBrand()
         => MyController<CarsController>
                 .Instance()
                 .WithData(PublicCars)
                 .Calling(c => c.All(AllCarsModel))
                 .ShouldReturn()
                 .View(view => view
                  .WithModelOfType<AllCarsViewModel>()
                  .Passing(c => c.Brand == "Bmw"));

        [Fact]
        public void ShouldReturnViewWhenForValidCurrentOtherBrand()
        => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .Calling(c => c.All(new AllCarsViewModel { Brand = "Mercedes" }))
                .ShouldReturn()
                .View(view => view
                 .WithModelOfType<AllCarsViewModel>()
                 .Passing(c => c.Brand == "Mercedes"));

        [Fact]
        public void ShouldReturnViewWhenBrandIsNull()
       => MyController<CarsController>
               .Instance()
               .WithData(PublicCars)
               .Calling(c => c.All(new AllCarsViewModel { }))
               .ShouldReturn()
               .View(view => view
                .WithModelOfType<AllCarsViewModel>()
                .Passing(c => c.Brand == null));

        [Fact]
        public void ShouldReturnViewWhenForValidCurrentSearch()
        => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .Calling(c => c.All(AllCarsModel))
                .ShouldReturn()
                .View(view => view
                 .WithModelOfType<AllCarsViewModel>()
                 .Passing(c => c.Search == "Audi"));

        [Fact]
        public void ShouldReturnViewWhenALlBrandsCountIsCurrect()
        => MyController<CarsController>
                .Instance()
                .WithData(PublicCars)
                .Calling(c => c.All(AllCarsModel))
                .ShouldReturn()
                .View(view => view
                 .WithModelOfType<AllCarsViewModel>()
                 .Passing(c => c.Brands.Should().HaveCount(1)));

        [Fact]
        public void ShouldReturnViewWhenSortingForDataCreated()
      => MyController<CarsController>
              .Instance()
              .WithData(PublicCars)
              .Calling(c => c.All(new AllCarsViewModel { Sorting = CarSorting.DateCreated }))
              .ShouldReturn()
              .View(view => view
               .WithModelOfType<AllCarsViewModel>()
               .Passing(c => c.Sorting == CarSorting.DateCreated));

        [Fact]
        public void ShouldReturnViewWhenSortingForYear()
     => MyController<CarsController>
             .Instance()
             .WithData(PublicCars)
             .Calling(c => c.All(new AllCarsViewModel { Sorting = CarSorting.Year }))
             .ShouldReturn()
             .View(view => view
              .WithModelOfType<AllCarsViewModel>()
              .Passing(c => c.Sorting == CarSorting.Year));

        [Fact]
        public void ShouldReturnViewWhenSortingForBrandModel()
      => MyController<CarsController>
              .Instance()
              .WithData(PublicCars)
              .Calling(c => c.All(new AllCarsViewModel { Sorting = CarSorting.BrandAndModel }))
              .ShouldReturn()
              .View(view => view
               .WithModelOfType<AllCarsViewModel>()
               .Passing(c => c.Sorting == CarSorting.BrandAndModel));

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
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateBrand()
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
                       .ContainingErrorFor(c => c.Model)
                       .ThatEquals(ErrorMessagesCarAddModel))
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
        public void PostAddCarForAuthorizedUserWhenUserIsBecomeDealerForErrorModelStateModelImageUrl()
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
            .RedirectToAction("DealerCars");

        [Fact]
        public void GetDealerAllCarForAuthorizedUsersWhenCarsIsZero()
            => MyController<CarsController>
                .Instance()
                  .WithData(LocationAdd)
                 .WithData(SecondDealaer)
                 .WithUser(TestUser.Identifier)
                  .Calling(c => c.DealerCars())
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
                 .Calling(c => c.DealerCars())
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
            .RedirectToAction("DealerCars");

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
            .RedirectToAction("DealerCars");

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

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidForId()
        => MyController<CarsController>
            .Instance()
            .WithData(OneCar)
            .Calling(x => x.Details(OneCar.Id))
          .ShouldReturn()
          .View(view => view
                    .WithModelOfType<CarServiceListingViewModel>()
                    .Passing(x => x.Id == OneCar.Id));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidForBrand()
        => MyController<CarsController>
            .Instance()
            .WithData(OneCar)
            .Calling(x => x.Details(OneCar.Id))
          .ShouldReturn()
          .View(view => view
                    .WithModelOfType<CarServiceListingViewModel>()
                    .Passing(x => x.Brand == "BMW"));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidForModel()
         => MyController<CarsController>
          .Instance()
          .WithData(OneCar)
          .Calling(x => x.Details(OneCar.Id))
        .ShouldReturn()
        .View(view => view
                  .WithModelOfType<CarServiceListingViewModel>()
                  .Passing(x => x.Model == "M5"));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForDoors()
        => MyController<CarsController>
         .Instance()
         .WithData(OneCar)
         .Calling(x => x.Details(OneCar.Id))
       .ShouldReturn()
       .View(view => view
                 .WithModelOfType<CarServiceListingViewModel>()
                 .Passing(x => x.Doors == 5));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForLugage()
       => MyController<CarsController>
        .Instance()
        .WithData(OneCar)
        .Calling(x => x.Details(OneCar.Id))
      .ShouldReturn()
      .View(view => view
                .WithModelOfType<CarServiceListingViewModel>()
                .Passing(x => x.Lugage == 4));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForIsPublic()
         => MyController<CarsController>
         .Instance()
         .WithData(OneCar)
         .Calling(x => x.Details(OneCar.Id))
         .ShouldReturn()
         .View(view => view
                .WithModelOfType<CarServiceListingViewModel>()
                .Passing(x => x.IsPublic == false));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForCategory()
        => MyController<CarsController>
        .Instance()
        .WithData(OneCar)
        .Calling(x => x.Details(OneCar.Id))
        .ShouldReturn()
        .View(view => view
               .WithModelOfType<CarServiceListingViewModel>()
               .Passing(x => x.Category == Category.Limousine));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForFuel()
       => MyController<CarsController>
       .Instance()
       .WithData(OneCar)
       .Calling(x => x.Details(OneCar.Id))
       .ShouldReturn()
       .View(view => view
              .WithModelOfType<CarServiceListingViewModel>()
              .Passing(x => x.Fuel == Fuel.Petrol));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForTransmission()
             => MyController<CarsController>
             .Instance()
             .WithData(OneCar)
             .Calling(x => x.Details(OneCar.Id))
             .ShouldReturn()
             .View(view => view
              .WithModelOfType<CarServiceListingViewModel>()
              .Passing(x => x.Transmission == Transmission.Automatic));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForPassager()
            => MyController<CarsController>
            .Instance()
            .WithData(OneCar)
            .Calling(x => x.Details(OneCar.Id))
            .ShouldReturn()
            .View(view => view
             .WithModelOfType<CarServiceListingViewModel>()
             .Passing(x => x.Passager == 4));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForRentPricePeerDay()
            => MyController<CarsController>
            .Instance()
            .WithData(OneCar)
            .Calling(x => x.Details(OneCar.Id))
            .ShouldReturn()
            .View(view => view
             .WithModelOfType<CarServiceListingViewModel>()
             .Passing(x => x.RentPricePerDay == 200));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForPriceCar()
            => MyController<CarsController>
            .Instance()
            .WithData(OneCar)
            .Calling(x => x.Details(OneCar.Id))
            .ShouldReturn()
            .View(view => view
             .WithModelOfType<CarServiceListingViewModel>()
             .Passing(x => x.Price == 30000));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForYear()
          => MyController<CarsController>
          .Instance()
          .WithData(OneCar)
          .Calling(x => x.Details(OneCar.Id))
          .ShouldReturn()
          .View(view => view
           .WithModelOfType<CarServiceListingViewModel>()
           .Passing(x => x.Year == 2005));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForLocationId()
          => MyController<CarsController>
          .Instance()
          .WithData(OneCar)
          .Calling(x => x.Details(OneCar.Id))
          .ShouldReturn()
          .View(view => view
           .WithModelOfType<CarServiceListingViewModel>()
           .Passing(x => x.LocationId == 2));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForImageUrl()
          => MyController<CarsController>
          .Instance()
          .WithData(OneCar)
          .Calling(x => x.Details(OneCar.Id))
          .ShouldReturn()
          .View(view => view
           .WithModelOfType<CarServiceListingViewModel>()
           .Passing(x => x.ImageUrl == "www.imagethebestBmw.com"));

        [Fact]
        public void ShouldReturnViewWhenCarFoundForDetailsValidModelForDescription()
         => MyController<CarsController>
         .Instance()
         .WithData(OneCar)
         .Calling(x => x.Details(OneCar.Id))
         .ShouldReturn()
         .View(view => view
          .WithModelOfType<CarServiceListingViewModel>()
          .Passing(x => x.Description == "The best car"));

    }
}
