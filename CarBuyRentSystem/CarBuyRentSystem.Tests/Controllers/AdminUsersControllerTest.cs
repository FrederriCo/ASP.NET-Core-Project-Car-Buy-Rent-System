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
        public void AdminAreaIndexShouldReturnViewWithDataForAllUsers()
           => MyController<UsersController>
               .Instance()               
               .WithData(OneDealaer)
               .Calling(x => x.AllUsers())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<UserServiceViewListingModel>>());

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllUsersCount()
           => MyController<UsersController>
               .Instance()
               .WithData(UserOne)
               .Calling(x => x.AllUsers())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<UserServiceViewListingModel>>()
                        .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllUsersCountIsZero()
           => MyController<UsersController>
             .Instance()             
             .Calling(x => x.AllUsers())
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<UserServiceViewListingModel>>()
                      .Passing(m => m.Should().HaveCount(0)));

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
        public void AdminAreaIndexShouldReturnViewWithDataForAllDealersValidCount()
           => MyController<UsersController>
               .Instance()
               .WithData(OneDealaer)
               .Calling(x => x.AllDealers())
               .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>()
                        .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaIndexShouldReturnViewWithDataForAllDealersWhenCountIsZero()
          => MyController<UsersController>
              .Instance()
              .Calling(x => x.AllDealers())
              .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>()
                       .Passing(m => m.Should().HaveCount(0)));

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
