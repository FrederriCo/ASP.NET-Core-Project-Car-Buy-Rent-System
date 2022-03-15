namespace CarBuyRentSystem.Core.Services.Cars
{
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface ICarService
    {
       // CarsViewModel All();

        IEnumerable<string> AllCarBrands();

        IEnumerable<CarServiceListingViewModel> ByUser(string userId);

        IEnumerable<CarServiceListingViewModel> GetCars(IQueryable<Car> carQuery);

    }
}
