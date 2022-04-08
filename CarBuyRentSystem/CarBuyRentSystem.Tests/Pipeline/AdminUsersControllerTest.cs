namespace CarBuyRentSystem.Tests.Pipeline
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;

    using CarBuyRentSystem.Areas.Admin.Controllers;
    using CarBuyRentSystem.Core.Models.Service.Users;

    using static Data.Cars;
    using static Data.Delars;
    using static Infrastructure.Data.WebConstants;

    public class AdminUsersControllerTest
    {
        [Fact]
        public void AdminAreaIndexReturnViewWithDataForAllUsers()
          => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/AllUsers")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.AllUsers())
            .Which(controller => controller
               .WithData(OneDealaer))
               .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<UserServiceViewListingModel>>());

        [Fact]
        public void AdminAreaIndexShouldReturnViewForAllUsersCount()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/AllUsers")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.AllUsers())
            .Which(controller => controller
               .WithData(UserOne))
             .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<UserServiceViewListingModel>>()
                      .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaIndexShouldReturnViewForAllUsersWhenCountIsZero()
        => MyMvc
            .Pipeline()
            .ShouldMap(request => request
             .WithPath("/Admin/Users/AllUsers")
             .WithUser(x => x.InRole(AdministratorRoleName))
             .WithAntiForgeryToken())
            .To<UsersController>(x => x.AllUsers())
           .Which(controller => controller
              .WithData(OneCar))
            .ShouldReturn()
            .View(view => view
                   .WithModelOfType<IEnumerable<UserServiceViewListingModel>>()
                     .Passing(m => m.Should().HaveCount(0)));

        [Fact]
        public void AdminAreaIndexReturnViewWithDataForAllDealers()
        => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/AllDealers")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.AllDealers())
            .Which(controller => controller
               .WithData(OneDealaer))
            .ShouldReturn()
            .View(view => view
                   .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>());

        [Fact]
        public void AdminAreaIndexReturnViewWithDataForAllDealersValidCount()
          => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/AllDealers")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.AllDealers())
            .Which(controller => controller
               .WithData(OneDealaer))
                .ShouldReturn()
              .View(view => view
                     .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>()
                       .Passing(m => m.Should().HaveCount(1)));

        [Fact]
        public void AdminAreaIndexReturnViewWithDataForAllDealersWhenCountIsSero()
         => MyMvc
            .Pipeline()
            .ShouldMap(request => request
             .WithPath("/Admin/Users/AllDealers")
             .WithUser(x => x.InRole(AdministratorRoleName))
             .WithAntiForgeryToken())
            .To<UsersController>(x => x.AllDealers())
           .Which(controller => controller
              .WithData(OneCar))
               .ShouldReturn()
             .View(view => view
                    .WithModelOfType<IEnumerable<DealerServiceViewListingModel>>()
                      .Passing(m => m.Should().HaveCount(0)));

        [Fact]
        public void AdminAreaReturnViewForDeleteUser()
       => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/DeleteUser/TestUser")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.DeleteUser(UserOne.Id))
            .Which(controller => controller
               .WithData(UserOne))
           .ShouldReturn()
           .RedirectToAction("AllUsers", "Users");

        [Fact]
        public void AdminAreaReturnViewErrorWhenUserForDeleteIsDealer()
            => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/DeleteUser/TestUser")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.DeleteUser(UserOne.Id))
            .Which(controller => controller
               .WithData(UserOne)
               .WithData(OneDealaer))
         .ShouldReturn()
         .RedirectToAction("ApplicationError");

        [Fact]
        public void AdminAreaReturnViewForDeleteDealer()
             => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/DeleteDealer/4")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.DeleteDealer(OneDealaer.Id))
            .Which(controller => controller              
               .WithData(OneDealaer))
          .ShouldReturn()
          .RedirectToAction("AllDealers", "Users");

        [Fact]
        public void AdminAreaViewErrorWhenDealerIsNotFoundForDelete()
         => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/DeleteDealer/5")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.DeleteDealer(5))
            .Which(controller => controller
               .WithData(OneDealaer))
        .ShouldReturn()
        .RedirectToAction("ApplicationError");

        [Fact]
        public void AdminAreaReturnViewError()
       => MyMvc
             .Pipeline()
             .ShouldMap(request => request
              .WithPath("/Admin/Users/ApplicationError")
              .WithUser(x => x.InRole(AdministratorRoleName))
              .WithAntiForgeryToken())
             .To<UsersController>(x => x.ApplicationError())
            .Which(controller => controller
               .WithData(OneDealaer))
           .ShouldReturn()
           .View();

    }
}
