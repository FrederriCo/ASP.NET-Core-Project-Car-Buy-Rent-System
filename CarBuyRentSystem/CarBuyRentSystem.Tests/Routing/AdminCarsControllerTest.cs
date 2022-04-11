namespace CarBuyRentSystem.Tests.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Areas.Admin.Controllers;
    public class AdminCarsControllerTest
    {
        [Fact]
        public void GetAllShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Admin/Cars/Index")
              .To<CarsController>(c => c.Index());

        [Fact]
        public void GetMineShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Cars/ChangeVisability/1")
                .To<CarsController>(c => c.ChangeVisability(1));

        [Fact]
        public void GetAllCarsShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/AllCars")
               .To<CarsController>(c => c.AllCars());

        [Fact]
        public void GetAllInformationShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/All")
               .To<CarsController>(c => c.All());

        [Fact]
        public void GetEditShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/Edit")
               .To<CarsController>(c => c.Edit());

        [Fact]
        public void GetDeleteShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Cars/Delete")
               .To<CarsController>(c => c.Delete());

        [Fact]
        public void GetRentedCarsShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Admin/Cars/RentedCars")
              .To<CarsController>(c => c.RentedCars());

        [Fact]
        public void GetSoldCarsCarsShouldBeRoutedCorrectly()
         => MyRouting
             .Configuration()
             .ShouldMap("/Admin/Cars/SoldCars")
             .To<CarsController>(c => c.SoldCars());       
    }
}
