namespace CarBuyRentSystem.Core.Services.UserService
{
    using System.Collections.Generic;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class UserService : DataService, IUserService
    {
        public UserService(CarDbContext db)
            : base(db)
        {

        }
       
        public IEnumerable<BuyCar> GetAllBoughtCarsByUser(string username)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BuyCar> GetAllSoldCars()
        {
            var soldCars = db
                    .BuyCars
                    .Include(c => c.Car)
                    .Include(u => u.User)
                    .ToList();

            return soldCars;
                        
        }

        public IEnumerable<RentCar> GetAllRentedCars()
        {
            var rentedCars = db
                    .RentCars
                    .Include(c => c.Car)
                    .Include(u => u.CarUser)
                    .ToList();

            return rentedCars;
        }

        public IEnumerable<RentCar> GetAllRentedCarsByUser(string username)
        {
            throw new System.NotImplementedException();
        }

       
    }
}
