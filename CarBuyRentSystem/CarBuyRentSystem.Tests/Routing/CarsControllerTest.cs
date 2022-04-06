namespace CarBuyRentSystem.Tests.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Controllers;
   
    using static Data.Cars;
    public class CarsControllerTest
    {
        [Fact]
        public void GetAddCarShouldBeRoutedCorrectly()
         => MyRouting
             .Configuration()
             .ShouldMap("/Cars/Add")
             .To<CarsController>(c => c.Add());

        [Fact]
        public void GetDealerCarShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Cars/DealerCar")
              .To<CarsController>(c => c.DealerCar());

        [Fact]
        public void GetEditCarShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Cars/Edit/3")
              .To<CarsController>(c => c.Edit(OneCar.Id));

        [Fact]
        public void GetDeleteCarShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Cars/Delete/3")
              .To<CarsController>(c => c.Delete(OneCar.Id));

        [Fact]
        public void GetDetailsCarShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Cars/Details/3")
              .To<CarsController>(c => c.Details(OneCar.Id));

    }
}
