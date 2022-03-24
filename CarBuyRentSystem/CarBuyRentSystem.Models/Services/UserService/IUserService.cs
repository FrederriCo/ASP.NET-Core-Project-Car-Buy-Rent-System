namespace CarBuyRentSystem.Core.Services.UserService
{
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    public interface IUserService
    {
       IEnumerable<RentCar> GetAllRentedCars();
       IEnumerable<BuyCar> GetAllSoldCars();
       IEnumerable<BuyCar> GetAllBoughtCarsByUser(string username);
       IEnumerable<RentCar> GetAllRentedCarsByUser(string username);

    }
}
