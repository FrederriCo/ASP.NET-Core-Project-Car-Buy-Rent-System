namespace CarBuyRentSystem.Core.Services.UserService
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CarBuyRentSystem.Infrastructure.Models;

    public interface IUserService
    {
       Task<IEnumerable<RentCar>> GetAllRentedCars();

       Task<IEnumerable<BuyCar>> GetAllSoldCars();

       Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username);

       Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username);

    }
}
