namespace CarBuyRentSystem.Core.Services.UserService
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserService : DataService, IUserService
    {
        public UserService(CarDbContext db)
            : base(db)
        {

        }
       
       public async Task<IEnumerable<BuyCar>> GetAllSoldCars()
        {
            var soldCars = await this.db
                    .BuyCars
                    .Include(c => c.Car)
                    .Include(u => u.User)
                    .ToListAsync();

            return soldCars;
                        
        }

        public async Task<IEnumerable<RentCar>> GetAllRentedCars()
        {
            var rentedCars = await this.db
                    .RentCars
                    .Include(c => c.Car)
                    .Include(u => u.CarUser)
                    .ToListAsync();

            return rentedCars;
        }

        public async Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username)
        {
            var allBoughtCars = await this.db
                    .BuyCars
                    .Include(c => c.Car)
                    .Where(u => u.User.Name == username)
                    .ToListAsync();

            return allBoughtCars;
        }
        public async Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username)
        {
            var allRentetCars = await this.db
                    .RentCars
                    .Include(c => c.Car)
                    .Where(u => u.CarUser.Name == username)
                    .ToListAsync();

            return allRentetCars;
        }

       
    }
}
