namespace CarBuyRentSystem.Core.Services.Cars
{
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface ICarService
    {
        // CarsViewModel All();

        CarDetailsServiceModel Details(int id);

        IEnumerable<CarServiceListingViewModel> AdminGetAllCar();
        bool IsByDealer(int carId, int dealerId);
        int Create(CreateCarServiceModel car);

        void Edit(CreateCarServiceModel car);

        bool LocationExsts(int locationId);

        IEnumerable<string> AllCarBrands();

        IEnumerable<CarServiceListingViewModel> ByUser(string userId);

        IEnumerable<CarServiceListingViewModel> GetCars(IQueryable<Car> carQuery);

        IEnumerable<CarLocationServiceModel> AllCarLocation();

    }
}
