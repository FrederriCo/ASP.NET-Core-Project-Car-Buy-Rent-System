namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Data;

    public class DealerService : DataService, IDealerService
    {
        public DealerService(CarDbContext db) 
            : base(db)
        {
        }

        public async Task<int> GetDealerId(string userId)
           => await this.db
                   .Dealers
                   .Where(x => x.UserId == userId)
                   .Select(x => x.Id)
                   .FirstOrDefaultAsync();

        public async Task<bool> IsDealer(string userId)
            => await this.db
                .Dealers
                .AnyAsync(d => d.UserId == userId);

        public async Task TotalUser()
           => await this.db.Users.CountAsync();
    }
}
