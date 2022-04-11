namespace CarBuyRentSystem.Tests.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Controllers;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    
    public class RentCarsControllerTest
    {
        [Fact]
        public void RentShuldBeMapped()
         => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/RentCars/Rent/32")
               .WithUser())
             .To<RentCarsController>(c => c.Rent(32));


        [Fact]
        public void PostRentShuldBeMapped()
         => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/RentCars/Rent/32")
                .WithUser()
               .WithMethod(HttpMethod.Post))
             .To<RentCarsController>(c => c.Rent(32));

        [Fact]
        public void PostRentedCarsRoutShouldBeMapeer()
           => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/RentCars/Rent")
               .WithUser()
               .WithMethod(HttpMethod.Post))
             .To<RentCarsController>(c => c.Rent(With.Any<RentCarBindingModel>()));
            
        [Fact]
        public void MyAllRentedCarsRoutShouldBeMapeer()
           => MyRouting
             .Configuration()
               .ShouldMap(request => request
              .WithPath("/RentCars/MyRentCars")
               .WithUser()
               .WithMethod(HttpMethod.Post))
             .To<RentCarsController>(c => c.MyRentCars());
    }
}
