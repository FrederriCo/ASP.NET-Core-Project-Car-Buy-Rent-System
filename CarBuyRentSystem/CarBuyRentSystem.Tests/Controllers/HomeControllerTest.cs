namespace CarBuyRentSystem.Tests.Controllers
{
    using Xunit;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using CarBuyRentSystem.Controllers;

    using CarBuyRentSystem.Core.Models.View.Cars;

    using static Data.Cars;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithValidCarsInDataBase()
           => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(c => c.WithData(TenPublicCars()))
                .ShouldReturn()
                .View(view => view
                       .WithModelOfType<IEnumerable<CarListingViewModel>>()
                       .Passing(m => m.Should().HaveCount(3)));

        [Fact]
        public void IndexShouldReturnViewWithNoCarsInDataBase()
          => MyMvc
               .Pipeline()
               .ShouldMap("/")
               .To<HomeController>(c => c.Index())
               .Which(c => c.WithoutUser())
                .ShouldReturn()
               .View(view => view
                      .WithModelOfType<IEnumerable<CarListingViewModel>>()
                      .Passing(m => m.Should().HaveCount(0)));

        [Fact]
        public void IndexRoutShouldBeMapeer()
        => MyRouting
            .Configuration()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index());


        [Fact]
        public void ErrorShuldReturnViewApplicationError()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/ApplicationError")
                .To<HomeController>(c => c.ApplicationError())
                .Which()
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorShuldReturnViewError()
           => MyMvc
               .Pipeline()
               .ShouldMap("/Home/Error")
               .To<HomeController>(c => c.Error())
               .Which()
               .ShouldReturn()
               .View();
    }
}

