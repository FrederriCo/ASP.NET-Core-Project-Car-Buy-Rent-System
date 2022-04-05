namespace CarBuyRentSystem.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    public static class Cars
    {
        public static IEnumerable<Car> PublicCars
         => Enumerable.Range(0, 15).Select(i => new Car
         {
             IsPublic = true,
             Model = "bmw",
             Brand = "Bmw",
             LocationId = 1,
             DealerId = 1,
             Description = "dfdsfdsfds",
             ImageUrl = "www.yahoo.com",
             Year = 2020,

         });

        public static Car OneCar
            => new Car
            {
                Id = 3,
                Brand = "BMW",
                Model = "M5",
                ImageUrl = "www.imagethebestBmw.com",
                Description = "The best car"
            };

        public static RentCar MyRentCars
            => new RentCar
            {
                Car = OneCar,
                CarUser = UserOne,
                CarId = OneCar.Id,
                UserId = UserOne.Id
            };

        public static BuyCar MyBuyCars
          => new BuyCar
          {
              Car = OneCar,               
              BuyCarId = OneCar.Id,
              CarId = OneCar.Id,
              UserId = UserOne.Id
          };

        public static CarUser UserOne
          => new CarUser()
          {
              Id = "TestId",
              UserName = "TestUser"
          };

        public static RentCarBindingModel RentCarBindig
            => new RentCarBindingModel
            {
                CarId = OneCar.Id,
                RentCarId = OneCar.Id
            };

        public static BuyCarBindingModel BuyCarBindig
          => new BuyCarBindingModel
          {
              CarId = OneCar.Id,
              BuyCarId = OneCar.Id
          };
    }
}
