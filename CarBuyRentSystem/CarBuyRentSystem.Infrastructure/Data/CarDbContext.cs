namespace CarBuyRentSystem.Data
{
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    

    public class CarDbContext : IdentityDbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; init; }
        public DbSet<Location> Locations { get; init; }
        public DbSet<BuyCar> BuyCars { get; init; }
        public DbSet<RentCar> RentCars { get; init; }
    }
}
