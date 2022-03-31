namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models;

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
        [InlineData("TopAuto","+359888888888")]
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
                .ValidModelState();
                

    }
}
