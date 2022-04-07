namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;

    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;

    using static Data.Delars;
    using static Infrastructure.Data.WebConstants;

    public class DealersControllerTest
    {
        [Fact]
        public void CreateShuldBeAuthorizedUsersAndReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request
                    .WithPath("/Dealers/Create")
                    .WithUser())
                .To<DealersController>(c => c.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(attribute => attribute
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void PostCreateDealerShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel()
          => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Dealers/Create")
                .WithMethod(HttpMethod.Post)
                .WithFormFields(CreateDealer)
                .WithUser()
                .WithAntiForgeryToken())
              .To<DealersController>(c => c.Create(CreateDealer))
              .Which()
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
    }
}
