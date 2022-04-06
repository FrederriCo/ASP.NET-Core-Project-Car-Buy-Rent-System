﻿namespace CarBuyRentSystem.Tests.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using CarBuyRentSystem.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void ApplicationErrorRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Home/ApplicationError")
               .To<HomeController>(c => c.ApplicationError());

        [Fact]
        public void ErrorRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Home/Error")
               .To<HomeController>(c => c.Error());
    }
}
