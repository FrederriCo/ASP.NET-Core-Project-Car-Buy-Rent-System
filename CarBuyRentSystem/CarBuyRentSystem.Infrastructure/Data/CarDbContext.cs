namespace CarBuyRentSystem.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using CarBuyRentSystem.Infrastructure.Models;

    public class CarDbContext : IdentityDbContext<CarUser>
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);          
           
            builder.Entity<Dealer>()
                    .HasOne<CarUser>()
                    .WithOne()
                    .HasForeignKey<Dealer>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Car>()
                    .HasOne(c => c.Dealer)
                    .WithMany(d => d.Cars)
                    .HasForeignKey(c => c.DealerId)
                    .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Car> Cars { get; init; }

        public DbSet<Dealer> Dealers { get; init; }

        public DbSet<Location> Locations { get; init; }

        public DbSet<BuyCar> BuyCars { get; init; }

        public DbSet<RentCar> RentCars { get; init; }

        public DbSet<CarUser> CarUsers { get; init; }
    }
}
