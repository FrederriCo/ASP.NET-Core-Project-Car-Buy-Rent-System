namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using System.Linq;
    using CarBuyRentSystem.Data;

    public class DealerService : IDealerService
    {
        private readonly CarDbContext db;

        public DealerService(CarDbContext db)
            => this.db = db;

        public int GetDealerId(string userId)
            => this.db
                   .Dealers
                   .Where(x => x.UserId == userId)
                   .Select(x => x.Id)
                   .FirstOrDefault();

        public bool IsDealer(string userId)
         => this.db
                .Dealers
                .Any(d => d.UserId == userId);

        public void TotalUser()
            => db.Users.Count();
    }
}
