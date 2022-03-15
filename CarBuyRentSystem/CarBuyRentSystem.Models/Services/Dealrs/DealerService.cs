namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using System.Linq;
    using CarBuyRentSystem.Data;

    public class DealerService : IDealerService
    {
        private readonly CarDbContext db;

        public DealerService(CarDbContext db)
            => this.db = db;
        public bool IsDealer(string userId)
         => this.db
                .Dealers
                .Any(d => d.UserId == userId);
                
    }
}
