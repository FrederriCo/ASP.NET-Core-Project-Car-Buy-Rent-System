namespace CarBuyRentSystem.Core.Services.Cars
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars.Enums;

    public interface ICarService
    {
        Task Add(Car car);

        Task<CarDetailsServiceModel> Details(int id);

        Task Edit(CreateCarServiceModel car);

        Task<bool> Buy(BuyCar buyCar, string username);

        Task<bool> Rent(RentCar rentCar, string username);

        Task Delete(int id);

        Task<CarQueryServiceModel> All(string brand = null,
                  string search = null,
                  CarSorting sorting = CarSorting.DateCreated,
                  int currentPage = 1,
                  int carsPerPage = int.MaxValue,
                  bool publicOnly = true);

        Task <IEnumerable<CarListingViewModel>> GetLastThreeCar();

        Task ChangeVisability(int carId);

        Task<Car> GetCarId(int id);

        Task<bool> IsByDealer(int carId, int dealerId);

        Task<bool> LocationExists(int locationId);

        TotalUserCar Total();

        Task<IEnumerable<string>> AllCarBrands();

        Task<IEnumerable<CarServiceListingViewModel>> AdminGetAllCar();

        Task<IEnumerable<CarServiceListingViewModel>> ByUser(string userId);

        Task<IEnumerable<CarServiceListingViewModel>> GetCars(IQueryable<Car> carQuery);

        Task<IEnumerable<CarLocationServiceModel>> AllCarLocation();  
    }
}
