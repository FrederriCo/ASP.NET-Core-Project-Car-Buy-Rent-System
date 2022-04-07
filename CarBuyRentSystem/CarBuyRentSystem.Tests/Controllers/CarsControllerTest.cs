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

    public class CarsControllerTest
    {
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
        public void AddCarForAuthorizedUserWhenUserIsBecomeDealer()
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
                        .RestrictingForAuthorizedRequests())
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
                        .ThatEquals(ErrorMessagesCarAdd))
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
                  .WithData(LocatinAdd)
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
        public void GetDealerAllCarForAuthorizedUsers()
            => MyController<CarsController>
                .Instance()
                  .WithData(LocatinAdd)
                 .WithData(SecondDealaer)
                 .WithUser(TestUser.Identifier)
                  .Calling(c => c.DealerCar())
               .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                     .WithModelOfType<IEnumerable<CarServiceListingViewModel>>());


    }
}
