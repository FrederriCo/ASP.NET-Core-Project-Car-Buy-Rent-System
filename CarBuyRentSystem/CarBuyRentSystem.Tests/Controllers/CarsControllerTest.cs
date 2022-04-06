namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;

    using CarBuyRentSystem.Core.Models.Cars;

    using static Data.Delars;
   
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
    }
}
