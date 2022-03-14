namespace CarBuyRentSystem.Infrastructure.Data
{
    using Microsoft.AspNetCore.Builder;
    using CarBuyRentSystem.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using CarBuyRentSystem.Infrastructure.Models;

    public static class ApplicationBuilderExtensions
    {
      
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
           using var scopedServices = app.ApplicationServices.CreateScope();

           var data =  scopedServices.ServiceProvider.GetService<CarDbContext>();

            data.Database.Migrate();

            SeedLocation(data);

            return app;
        }

        private static void SeedLocation(CarDbContext data)
        {           

            if (data.Locations.Any())
            {
                return;
            }

            data.Locations.AddRange(new[]
            {
                new Location { Name = "Sofia" },
                new Location { Name = "Plovdiv" },
                new Location { Name = "Varna" },
                new Location { Name = "Bourgas" },
                new Location { Name = "Stara Zagora" },
                new Location { Name = "Pleven" },
                new Location { Name = "Yambol" },
                new Location { Name = "Laki" }
            });

            data.SaveChanges();
        }
    }
}
