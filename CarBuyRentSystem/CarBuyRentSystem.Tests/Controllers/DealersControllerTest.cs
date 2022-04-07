namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;

    using CarBuyRentSystem.Core.Models;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;

    using static Data.Delars;
    using static Infrastructure.Data.WebConstants;

    public class DealersControllerTest
    {
        [Fact]
        public void CreateDealerShouldBeForAuthorizedUsersRetrunView()
         => MyController<DealersController>
             .Instance()
             .Calling(c => c.Create())
             .ShouldHave()
             .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests());


        [Fact]
        public void GetCreateShouldReturnView()
            => MyController<DealersController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldReturn()
                .View();


        [Fact]
        public void CreateDealerShouldBeForAuthorizedUsersNotValidModelState()
           => MyMvc.
                 Controller<DealersController>()
                .WithUser()
                .Calling(c => c.Create(CreateDealerNotValidModel))
                .ShouldHave()
                 .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post)
                  .RestrictingForAuthorizedRequests())
                .ModelState(modelState => modelState
                    .For<DealerFormServiceModel>()
                    .ContainingNoErrorFor(m => m.Name)
                    .AndAlso()
                    .ContainingErrorFor(m => m.PhoneNumber)
                    .ThatEquals(ErrorMeesagessDealer))
                 .AndAlso()
                 .ShouldReturn()
                .View(CreateDealerNotValidModel);


        [Fact]
        public void PostCreateDealerShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel()
            => MyController<DealersController>
                .Instance(controller => controller
                    .WithUser(TestUser.Username, TestUser.Identifier))
                .Calling(c => c.Create(CreateDealer))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .Data(data => data.WithSet<Dealer>(d =>
                {
                    d.Any(d => d.Name == CreateDealer.Name &&
                          d.PhoneNumber == CreateDealer.PhoneNumber &&
                          d.UserId == TestUser.Identifier);
                }))
            .TempData(tempData => tempData
                .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<CarsController>(c => c.All(With.Any<AllCarsViewModel>())));


        [Fact]
        public void PostCreateDealerShouldBeForAuthorizedUsersAndDealerAreExistsRedirectToApplicationError()
           => MyController<DealersController>
               .Instance(controller => controller
                   .WithUser(TestUser.Username, TestUser.Identifier)
                    .WithData(OneDealaer))
               .Calling(c => c.Create(CreateDealer))
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ApplicationError", "Home");
    }
}
