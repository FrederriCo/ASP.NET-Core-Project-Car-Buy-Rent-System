namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models;

    using CarBuyRentSystem.Infrastructure.Models;

    using static Infrastructure.Data.WebConstants;
    using CarBuyRentSystem.Core.Models.View.Cars;

    public class DealersControllerTest
    {
        [Fact]
        public void GetCreateDealerShuldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("Dealers/Create")
                .To<DealersController>(c => c.Create());

        [Fact]
        public void PostCreateDealerShuldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                .WithPath("/Dealers/Create")
                .WithMethod(HttpMethod.Post))
               .To<DealersController>(c => c.Create(With.Any<DealerFormServiceModel>()));

        [Fact]
        public void CreateDealerShouldBeForAuthorizedUsers()
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

        [Theory]
        [InlineData("TopAuto", "+3599999999")]
        public void PostCreateDealerShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(string dealerName, string phoneNumber)
            => MyController<DealersController>
                .Instance(controller => controller
                    .WithUser(TestUser.Username, TestUser.Identifier))
                .Calling(c => c.Create(new DealerFormServiceModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .Data(data => data.WithSet<Dealer>(d =>
                {
                    d.Any(d => d.Name == dealerName &&
                          d.PhoneNumber == phoneNumber &&
                          d.UserId == TestUser.Identifier);
                }))
            .TempData(tempData => tempData
                .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<CarsController>(c => c.All(With.Any<AllCarsViewModel>())));


    }
}
