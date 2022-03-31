namespace CarBuyRentSystem.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using CarBuyRentSystem.Infrastructure.Models;
    using System.Threading.Tasks;
    using CarBuyRentSystem.Tests.Models;
    using System;

    public static class Cars
    {
        
        public static IEnumerable<Car> TenPublicCars()
         => Enumerable.Range(0, 10).Select(i => new Car());
        //var data = DataBaseMock.Instane;

       // var cars = Enumerable.Range(0, 10).Select(i => new Car());
            //data.Cars.AddRange(cars);
            //data.SaveChanges();

            //return data.Cars;
        
    }
}
