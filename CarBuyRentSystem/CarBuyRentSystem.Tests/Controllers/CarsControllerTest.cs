namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;

    public class CarsControllerTest
    {
        [Fact]
        public void RentACarForAuthorizedUsers()
                => MyController<CarsController>
                                  
                    .Instance()
                     .WithUser(TestUser.Identifier)
                    .Calling(c => c.Add())
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests());
    }
}
