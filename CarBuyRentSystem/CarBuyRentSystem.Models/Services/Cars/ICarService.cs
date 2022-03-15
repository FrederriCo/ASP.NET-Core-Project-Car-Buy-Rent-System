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

        int Create(CarDetailsServiceModel car);

        bool LocationExsts(int locationId);

        IEnumerable<string> AllCarBrands();

        IEnumerable<CarServiceListingViewModel> ByUser(string userId);

        IEnumerable<CarServiceListingViewModel> GetCars(IQueryable<Car> carQuery);

        IEnumerable<CarLocationServiceModel> AllCarLocation();

    }
}
