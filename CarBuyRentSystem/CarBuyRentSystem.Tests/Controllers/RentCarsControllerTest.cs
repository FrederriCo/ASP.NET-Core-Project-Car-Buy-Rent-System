namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Linq;

    public class RentCarsControllerTest
    {
        [Fact]
        public void PostRentShuldBeMapped()
          => MyRouting
              .Configuration()
                .ShouldMap(request => request
               .WithPath("/RentCars/Rent/32")
               .WithMethod(HttpMethod.Post))
              .To<RentCarsController>(c => c.Rent(32));


        [Fact]
        public void RentACarForAuthorizedUsers()
               => MyController<RentCarsController>
                   .Instance()
                   .Calling(c => c.Rent(32))
                   .ShouldHave()
                   .ActionAttributes(attributes => attributes
                       .RestrictingForAuthorizedRequests());
                    
                    
                       

    }
}
