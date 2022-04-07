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
        public void IndexActionShouldReturnCorretView()
             => MyController<HomeController>
                 .Instance(i => i
                     .WithData(PublicCars)
                     .WithUser())
                 .Calling(c => c.Index())
                 .ShouldReturn()
                 .View(v => v
                       .WithModelOfType<IEnumerable<CarListingViewModel>>()
                       .Passing(model => model.Should().HaveCount(3)));


        [Fact]
        public void ErrorApplicationShouldReturnView()
            => MyController<HomeController>
                .Instance()                
                .Calling(c => c.ApplicationError())
                .ShouldReturn()
                .View();


        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}

