namespace CarBuyRentSystem.Infrastructure.Data
{
    using Microsoft.AspNetCore.Builder;
    using CarBuyRentSystem.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity;

    using static WebConstants;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<CarDbContext>();

            data.Database.Migrate();

            SeedLocation(data);
            SeedAdministrator(data, serviceProvider);

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

        private static void SeedAdministrator(CarDbContext db, IServiceProvider services)
        {
            var userManeger = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManeger = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManeger.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManeger.CreateAsync(role);

                const string adminEmil = "admin@admin.com";
                const string adminUser = "admin";
                const string adminPassword = "admin123";

                var user = new CarUser
                {
                    UserName = adminUser,
                    Email = adminEmil
                };

                await userManeger.CreateAsync(user, adminPassword);

                await userManeger.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
