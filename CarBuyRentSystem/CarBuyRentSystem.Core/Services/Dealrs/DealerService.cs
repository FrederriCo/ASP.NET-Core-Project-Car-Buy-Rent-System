namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using CarBuyRentSystem.Core.Models;
    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;

    public class DealerService : DataService, IDealerService
    {
        private readonly IMapper mapper;
        public DealerService(CarDbContext db, IMapper mapper)
            : base(db)
        {
            this.mapper = mapper;
        }

        public async Task<bool> Create(DealerFormServiceModel dealer, string userId)
        {
            var userAlreadyDealer = await this.db
                                   .Dealers
                                   .AnyAsync(x => x.UserId == userId);
            if (userAlreadyDealer)
            {
                return false;
            }

            var dealerAdd = mapper.Map<Dealer>(dealer);
            dealerAdd.UserId = userId;

            db.Dealers.Add(dealerAdd);
            db.SaveChanges();

            return true;
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
