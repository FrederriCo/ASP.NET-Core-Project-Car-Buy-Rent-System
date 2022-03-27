namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Core.Models;
    using AutoMapper;
    using CarBuyRentSystem.Infrastructure.Models;

    public class DealerService : DataService, IDealerService
    {
        private readonly IMapper mapper;
        public DealerService(CarDbContext db, IMapper mapper) 
            : base(db)
        {
            this.mapper = mapper;
        }

        public bool Create(DealerFormServiceModel dealer, string userId)
        {
            var userAlreadyDealer = this.db
                                      .Dealers
                                      .Any(x => x.UserId == userId);
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

        public int GetDealerId(string userId)
           =>  this.db
                   .Dealers
                   .Where(x => x.UserId == userId)
                   .Select(x => x.Id)
                   .FirstOrDefault();

        public bool IsDealer(string userId)
            => this.db
                .Dealers
                .Any(d => d.UserId == userId);

        public void TotalUser()
           =>  this.db.Users.CountAsync();
    }
}
