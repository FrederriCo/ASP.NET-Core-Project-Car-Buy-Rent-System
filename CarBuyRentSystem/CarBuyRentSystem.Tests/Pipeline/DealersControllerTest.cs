namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;

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
    }
}
