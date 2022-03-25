namespace CarBuyRentSystem.Core.Services.Cars
{
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICarService
    {
        // CarsViewModel All();

        Task<bool> Buy(BuyCar buyCar, string username);

        Task<bool> Rent(RentCar rentCar, string username);

        CarDetailsServiceModel Details(int id);

        void ChangeVisability(int carId);

        Car GetCarId(int id);

        IEnumerable<CarServiceListingViewModel> AdminGetAllCar();

        bool IsByDealer(int carId, int dealerId);

        int Create(CreateCarServiceModel car);

        void Edit(CreateCarServiceModel car);

        bool LocationExsts(int locationId);

        void Delete(int id);

         TotalUserCar Total();
        IEnumerable<string> AllCarBrands();

        IEnumerable<CarServiceListingViewModel> ByUser(string userId);

        IEnumerable<CarServiceListingViewModel> GetCars(IQueryable<Car> carQuery);

        IEnumerable<CarLocationServiceModel> AllCarLocation();

    }
}
