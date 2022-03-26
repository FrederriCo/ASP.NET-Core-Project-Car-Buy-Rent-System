namespace CarBuyRentSystem.Infrastructure.Data
{
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;         

            MigrateDatabase(services);
            SeedLocation(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarDbContext>();

            data.Database.Migrate();
        }
        private static void SeedLocation(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarDbContext>();

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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManeger = services.GetRequiredService<UserManager<CarUser>>();
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
                const string adminUser = "admin@admin.com";
                const string adminName = "admin@admin.com";
                const string adminPassword = "bg123456";
                const string adminAddress = "Sofia";
                

                var user = new CarUser
                {
                    UserName = adminUser,
                    Name = adminName,
                    Email = adminEmil,
                    Address = adminAddress,
                    Balance = 9999999m
                    
                };

                await userManeger.CreateAsync(user, adminPassword);

                await userManeger.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
