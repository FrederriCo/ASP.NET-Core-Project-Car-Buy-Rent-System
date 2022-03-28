namespace CarBuyRentSystem.Core.Services.Cars
{
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICarService
    {
        // CarsViewModel All();
        //int Create(CreateCarServiceModel car);

        Task Add(Car car);

        Task Edit(CreateCarServiceModel car);

        Task<bool> Buy(BuyCar buyCar, string username);

        Task<bool> Rent(RentCar rentCar, string username);

        CarDetailsServiceModel Details(int id);

        Task<IEnumerable<CarListingVIewModel>> GetLastThreeCar();

        Task ChangeVisability(int carId);

        Task<Car> GetCarId(int id);

        bool IsByDealer(int carId, int dealerId);      

        Task<bool> LocationExists(int locationId);

        Task Delete(int id);

        TotalUserCar Total();

        Task<IEnumerable<string>> AllCarBrands();

        Task<IEnumerable<CarServiceListingViewModel>> AdminGetAllCar();

        Task<IEnumerable<CarServiceListingViewModel>> ByUser(string userId);

        Task<IEnumerable<CarServiceListingViewModel>> GetCars(IQueryable<Car> carQuery);

        Task<IEnumerable<CarLocationServiceModel>> AllCarLocation();


    }
}
