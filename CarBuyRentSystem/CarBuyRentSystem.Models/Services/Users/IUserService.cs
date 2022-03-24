namespace CarBuyRentSystem.Core.Services.UserService
{
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
       Task<IEnumerable<RentCar>> GetAllRentedCars();

       Task<IEnumerable<BuyCar>> GetAllSoldCars();

       Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username);

       Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username);

    }
}
