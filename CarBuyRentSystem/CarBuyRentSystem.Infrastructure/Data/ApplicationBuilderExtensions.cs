namespace CarBuyRentSystem.Infrastructure.Data
{
    using Microsoft.AspNetCore.Builder;
    using CarBuyRentSystem.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
           using var scopedServices = app.ApplicationServices.CreateScope();

           var data =  scopedServices.ServiceProvider.GetService<CarDbContext>();

            data.Database.Migrate();

            return app;
        }
    }
}
