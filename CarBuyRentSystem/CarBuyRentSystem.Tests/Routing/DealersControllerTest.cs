namespace CarBuyRentSystem.Tests.Routing
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
    }
}
