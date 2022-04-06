namespace CarBuyRentSystem.Tests.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Areas.Admin.Controllers;

    public class AdminUsersControllerTest
    {
        [Fact]
        public void GetAllUsersShouldBeRoutedCorrectly()
        => MyRouting
            .Configuration()
            .ShouldMap("/Admin/Users/AllUsers")
            .To<UsersController>(c => c.AllUsers());

        [Fact]
        public void GetAllDealersShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap("/Admin/Users/AllDealers")
              .To<UsersController>(c => c.AllDealers());

        [Fact]
        public void GetDeleteUserShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Users/DeleteUser/TestUser")
               .To<UsersController>(c => c.DeleteUser("TestUser"));

        [Fact]
        public void GetDeleteDealerShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Users/DeleteDealer/5")
               .To<UsersController>(c => c.DeleteDealer(5));

        [Fact]
        public void ApplicationErrorShouldBeRoutedCorrectly()
           => MyRouting
               .Configuration()
               .ShouldMap("/Admin/Users/ApplicationError")
               .To<UsersController>(c => c.ApplicationError());
    }
}
