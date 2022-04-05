namespace CarBuyRentSystem.Core.Services.UserService
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.Service.Users;

    public class UserService : DataService, IUserService
    {
        public UserService(CarDbContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<BuyCar>> GetAllSoldCars()
            => await this.db
                    .BuyCars
                    .Include(c => c.Car)
                    .Include(u => u.User)
                    .ToListAsync();
        
        public async Task<IEnumerable<RentCar>> GetAllRentedCars()
            => await this.db
                    .RentCars
                    .Include(c => c.Car)
                    .Include(u => u.CarUser)
                    .ToListAsync();           
        

        public async Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username)
            => await this.db
                    .BuyCars
                    .Include(c => c.Car)
                    .Where(u => u.User.Id == username)
                    .ToListAsync();
     
        public async Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username)
            => await this.db
                    .RentCars
                    .Include(c => c.Car)
                    .Where(u => u.CarUser.Id == username)
                    .ToListAsync();

        public async Task<IEnumerable<UserServiceViewListingModel>> GetAllUser()
            => await db.CarUsers
                 .Select(x => new UserServiceViewListingModel
                 {
                     Id = x.Id,
                     UserName = x.UserName,
                     Email = x.Email,
                     Address = x.Address,
                     Balance = x.Balance
                 })
                 .ToListAsync();

        public async Task<IEnumerable<DealerServiceViewListingModel>> GetAllDealer()
             => await db.Dealers
                .Select(x => new DealerServiceViewListingModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.PhoneNumber,
                    CarCount = x.Cars.Count()

                })
                .ToListAsync();

        public async Task<bool> DeleteUser(string id)
        {
            var dealer = await db.Dealers
                        .FirstOrDefaultAsync(c => c.UserId == id);

            var user = await db.CarUsers
                        .FirstOrDefaultAsync(c => c.Id == id);

            if (dealer != null)
            {
                return false;
            }

            db.CarUsers.Remove(user);
            db.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteDealrs(int dealerId)
        {
            var dealer = await db.Dealers
                .FirstOrDefaultAsync(c => c.Id == dealerId);

            if (dealer == null)
            {
                return false;
            }

            db.Dealers.Remove(dealer);
            db.SaveChanges();

            return true;
        }
    }
}
