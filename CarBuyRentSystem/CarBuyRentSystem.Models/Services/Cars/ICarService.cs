using System.Collections.Generic;

namespace CarBuyRentSystem.Core.Services.Cars
{
    public interface ICarService
    {
        //AllCarsViewModel All();

        IEnumerable<string> AllCarBrands();
    }
}
