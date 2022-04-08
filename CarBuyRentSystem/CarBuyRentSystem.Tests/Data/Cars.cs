namespace CarBuyRentSystem.Tests.Data
{
    using System.Linq;
    using System.Collections.Generic;

    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;
    using CarBuyRentSystem.Core.Models.Cars;
    using System;
    using CarBuyRentSystem.Infrastructure.Models.Enums;

    public static class Cars
    {
        public static string ErrorMessagesCarAdd = "The length mst be between 2 and 30";

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
                Description = "The best car",
                DealerId = 4

            };

        public static Car SecondCar
           => new Car
           {
               Id = 3,
               Brand = "BMW",
               Model = "M5",
               ImageUrl = "www.imagethebestBmw.com",
               Description = "The best car",
               DealerId = 3
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
              Id = "TestUser",
              UserName = "TestUser"
          };

        public static CarUser UserSecond
          => new CarUser()
          {
              Id = "TestId",
              UserName = "TestUser"
          };

        public static RentCarBindingModel RentCarBindig
            => new RentCarBindingModel
            {
                CarId = OneCar.Id,
                RentCarId = OneCar.Id, 
               // Car = SecondCar
                
            };

        public static BuyCarBindingModel BuyCarBindig
          => new BuyCarBindingModel
          {
              CarId = OneCar.Id,
              BuyCarId = OneCar.Id
          };

        public static AddCarFormServiceModel AddCarService
            => new AddCarFormServiceModel
            {
                Brand = "Brand",
                Model = "bmw",
                Year = 2020,
                ImageUrl = "https://www.bmwgroup.com/content/dam/grpw/websites/bmwgroup_com/brands/einstiegsseite/1280x854_P90351044_highRes_the-new-bmw-8-series.jpg",
                Description = "dfdsfdsfdssdvsvfvdfvdfvdfvfdvf",
                Category = Category.Cabriolet,
                Fuel = Fuel.Diesel,
                Transmission = Transmission.Automatic,
                Lugage = 5,
                Doors = 4,
                Passager = 4,
                Price = 23230,
                RentPricePerDay = 500,
                LocationId = 4,
                Locations = null
            };

        public static AddCarFormServiceModel NotValidModelAddCar
            => new AddCarFormServiceModel
            {
                Model = "Mercedes",
                Lugage = 5,
                Brand = "B",
                Price = 23230,
                RentPricePerDay = 500,
                Description = "dfdsfdsfds",
                ImageUrl = "www.yahoo.com",

                Year = 2020,

            };

        public static CarDetailsServiceModel CarDetails
             => new CarDetailsServiceModel
             {
                 Id = 3,
                 Brand = "BMW",
                 Model = "M5",
                 ImageUrl = "www.imagethebestBmw.com",
                 Description = "The best car",
                 UserId = "TestId"
             };

        public static CreateCarServiceModel CarEdit
            => new CreateCarServiceModel
            {
                Id = 3,
                Brand = "Brand",
                Model = "bmw",
                Year = 2020,
                ImageUrl = "https://www.bmwgroup.com/content/dam/grpw/websites/bmwgroup_com/brands/einstiegsseite/1280x854_P90351044_highRes_the-new-bmw-8-series.jpg",
                Description = "dfdsfdsfdssdvsvfvdfvdfvdfvfdvf",
                Category = Category.Cabriolet,
                Fuel = Fuel.Diesel,
                Transmission = Transmission.Automatic,
                Lugage = 5,
                Doors = 4,
                Passager = 4,
                Price = 23230,
                RentPricePerDay = 500,
                LocationId = 2,
                DealerId = 4,
                 
            };

        public static CreateCarServiceModel NotValidCarEdit
         => new CreateCarServiceModel
         {
             Id = 3,            
             Lugage = 5,
             Doors = 4,
             Passager = 4,
              Description = "a",
             Price = 23230,
             RentPricePerDay = 500,
             LocationId = 2,
             DealerId = 4,

         };

        public static AllCarsViewModel AllCarsModel
            => new AllCarsViewModel
            {
                  
            };

        public static IEnumerable<Location> LocatinAdd
            => Enumerable.Range(0, 5).Select(i => new Location
            {
                Name = "Plovdiv"
            });               
    }
}
