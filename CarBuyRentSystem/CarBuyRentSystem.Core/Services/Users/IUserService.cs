namespace CarBuyRentSystem.Core.Services.UserService
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.Service.Users;

    public interface IUserService
    {
        Task<IEnumerable<UserServiceViewListingModel>> GetAllUser();

        Task<IEnumerable<DealerServiceViewListingModel>> GetAllDealer();

        Task<bool> DeleteUser(string id);

        Task<bool> DeleteDealrs(int dealerId);

        Task<IEnumerable<RentCar>> GetAllRentedCars();

        Task<IEnumerable<BuyCar>> GetAllSoldCars();

        Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username);

        Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username);

    }
}
