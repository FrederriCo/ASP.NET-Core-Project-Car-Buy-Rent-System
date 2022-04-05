namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Areas.Admin.Controllers;
    using CarBuyRentSystem.Core.Models.Service.Users;

    using static Data.Cars;
    using static Data.Delars;

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

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllUsers()
           => MyController<UsersController>
               .Instance()               
               .WithData(OneDealaer)
               .Calling(x => x.AllUsers())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<UserServiceViewListingModel>>());

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllUsersCountTest()
           => MyController<UsersController>
               .Instance()
               .WithData(UserOne)
               .Calling(x => x.AllUsers())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<UserServiceViewListingModel>>()
                        .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllDealers()
         => MyController<UsersController>
             .Instance()
             .WithData(OneDealaer)
             .Calling(x => x.AllDealers())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>());

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllDealersCountTest()
           => MyController<UsersController>
               .Instance()
               .WithData(OneDealaer)
               .Calling(x => x.AllDealers())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>()
                        .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaShouldReturnViewForDeleteUser()
        => MyController<UsersController>
            .Instance()
            .WithData(UserOne)
            .Calling(x => x.DeleteUser(UserOne.Id))
            .ShouldReturn()
            .RedirectToAction("AllUsers", "Users");

        [Fact]
        public void AdminAreaShouldReturnViewErrorWhenUserForDeleteIsDealer()
       => MyController<UsersController>
           .Instance()
           .WithData(OneDealaer)
            .WithData(UserOne)
           .Calling(x => x.DeleteUser(UserOne.Id))
           .ShouldReturn()
           .RedirectToAction("ApplicationError");

        [Fact]
        public void AdminAreaShouldReturnViewForDeleteDealer()
       => MyController<UsersController>
           .Instance()
           .WithData(OneDealaer)
           .Calling(x => x.DeleteDealer(OneDealaer.Id))
           .ShouldReturn()
           .RedirectToAction("AllDealers", "Users");

        [Fact]
        public void AdminAreaShouldReturnViewErrorWhenDealerIsNotFoundForDelete()
       => MyController<UsersController>
           .Instance()
           .WithData(OneDealaer)
           .Calling(x => x.DeleteDealer(5))
           .ShouldReturn()
           .RedirectToAction("ApplicationError");
    }
}
