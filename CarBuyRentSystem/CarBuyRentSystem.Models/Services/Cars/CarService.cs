using CarBuyRentSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace CarBuyRentSystem.Core.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly CarDbContext db;

        public CarService(CarDbContext db)
           => this.db = db;
        
        public IEnumerable<string> AllCarBrands()
           => db
              .Cars
              .Select(c => c.Brand)
              .Distinct()
              .OrderBy(b => b)
              .ToList();
    }
}
