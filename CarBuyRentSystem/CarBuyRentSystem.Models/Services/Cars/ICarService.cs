using System.Collections.Generic;

namespace CarBuyRentSystem.Core.Services.Cars
{
    public interface ICarService
    {
       // CarsViewModel All();

        IEnumerable<string> AllCarBrands();
    }
}
