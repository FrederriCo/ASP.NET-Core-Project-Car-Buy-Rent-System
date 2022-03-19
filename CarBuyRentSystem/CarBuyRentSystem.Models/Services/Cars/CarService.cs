﻿namespace CarBuyRentSystem.Core.Services.Cars
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class CarService : ICarService
    {
        private readonly CarDbContext db;
        private readonly IConfigurationProvider mapper;

        public CarService(CarDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<CarServiceListingViewModel> AdminGetAllCar()
         => db.Cars.ProjectTo<CarServiceListingViewModel>(this.mapper).ToList();
                //.Select(x => new CarDetailsServiceModel
                //{
                //    Brand = x.Brand
                //}).ToList();

        //public AllCarsViewModel All()
        //{
        //    var carsQuery = this.db.Cars.AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(query.Brand))
        //    {
        //        carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
        //    }

        //    if (!string.IsNullOrWhiteSpace(query.Search))
        //    {
        //        carsQuery = carsQuery.Where(c =>
        //                (c.Model + c.Brand).ToLower().Contains(query.Search.ToLower())
        //                || c.Description.ToLower().Contains(query.Search.ToLower())
        //            );
        //    }

        //    carsQuery = query.Sorting switch
        //    {
        //        CarSorting.Price => carsQuery.OrderByDescending(x => x.RentPricePerDay),
        //        CarSorting.BrandAnyModel => carsQuery.OrderBy(x => x.Brand).ThenBy(c => c.Model),
        //        CarSorting.Year or _ => carsQuery.OrderByDescending(c => c.Year)
        //    };

        //    var totalCars = carsQuery.Count();

        //    var cars = carsQuery
        //        .Skip((query.CurentPage - 1) * AllCarsViewModel.CarPerPage)
        //        .Take(AllCarsViewModel.CarPerPage)
        //        .Select(c => new CarListingVIewModel
        //        {
        //            Id = c.Id,
        //            Brand = c.Brand,
        //            Model = c.Model,
        //            Year = c.Year,
        //            Category = c.Category,
        //            Fuel = c.Fuel,
        //            Transmission = c.Transmission,
        //            ImageUrl = c.ImageUrl,
        //            Lugage = c.Lugage,
        //            Doors = c.Doors,
        //            Passager = c.Passager,
        //            Locaton = c.Location.Name,
        //            Price = c.Price,
        //            RentPricePerDay = c.RentPricePerDay
        //        })
        //        .ToList();
        //}

        public IEnumerable<string> AllCarBrands()
           => db
              .Cars
              .Select(c => c.Brand)
              .Distinct()
              .OrderBy(b => b)
              .ToList();

        public IEnumerable<CarLocationServiceModel> AllCarLocation()
            => this.db
            .Locations
            .Select(l => new CarLocationServiceModel
            {
                Id = l.Id,
                Name = l.Name
            })
            .ToList();

        public IEnumerable<CarServiceListingViewModel> ByUser(string userId)
            => this.GetCars(this.db
                            .Cars
                            .Where(c => c.Dealer.UserId == userId));

        public void ChangeVisability(int carId)
        {
            var car = db.Cars.Find(carId);

            car.IsPublic = !car.IsPublic;

            db.SaveChanges();
        }

        public int Create(CreateCarServiceModel car)
        {
            var carAdd = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                ImageUrl = car.ImageUrl,
                Description = car.Description,
                Category = car.Category,
                Fuel = car.Fuel,
                Transmission = car.Transmission,
                Lugage = car.Lugage,
                Doors = car.Doors,
                Passager = car.Passager,
                RentPricePerDay = car.RentPricePerDay,
                Price = car.Price,
                LocationId = car.LocationId

            };

            db.Cars.Add(carAdd);
            db.SaveChanges();

            return carAdd.Id;

        }

        public CarDetailsServiceModel Details(int id)
            => this.db.Cars
                    .Where(c => c.Id == id)
                    .ProjectTo<CarDetailsServiceModel>(this.mapper) //--With AutoMapper
                    //.Select(c => new CarDetailsServiceModel
                    //{
                    //    Id = c.Id,
                    //    Brand = c.Brand,
                    //    Model = c.Model,
                    //    Year = c.Year,
                    //    Category = c.Category,
                    //    Fuel = c.Fuel,
                    //    Transmission = c.Transmission,
                    //    ImageUrl = c.ImageUrl,
                    //    Lugage = c.Lugage,
                    //    Doors = c.Doors,
                    //    Passager = c.Passager,
                    //    LocationId = c.LocationId,
                    //    Locaton = c.Location.Name,
                    //    Price = c.Price,
                    //    RentPricePerDay = c.RentPricePerDay,
                    //    Description = c.Description,
                    //    DealerId = c.DealerId,
                    //    DealerName = c.Dealer.Name,
                    //    UserId = c.Dealer.UserId

                    //}) -- Without AutoMapper
                    .FirstOrDefault();

        public void Edit(CreateCarServiceModel car)
        {
            var carData = this.db.Cars.Find(car.Id);

            carData.Brand = car.Brand;
            carData.Model = car.Model;
            carData.Year = car.Year;
            carData.ImageUrl = car.ImageUrl;
            carData.Description = car.Description;
            carData.Category = car.Category;
            carData.Fuel = car.Fuel;
            carData.Transmission = car.Transmission;
            carData.Lugage = car.Lugage;
            carData.Doors = car.Doors;
            carData.Passager = car.Passager;
            carData.RentPricePerDay = car.RentPricePerDay;
            carData.Price = car.Price;
            carData.LocationId = car.LocationId;

            db.SaveChanges();

        }

        public IEnumerable<CarServiceListingViewModel> GetCars(IQueryable<Car> carQuery)
         => carQuery
           .Select(c => new CarServiceListingViewModel
           {
               Id = c.Id,
               Brand = c.Brand,
               Model = c.Model,
               Year = c.Year,
               Category = c.Category,
               Fuel = c.Fuel,
               Transmission = c.Transmission,
               ImageUrl = c.ImageUrl,
               Lugage = c.Lugage,
               Doors = c.Doors,
               Passager = c.Passager,
               Locaton = c.Location.Name,
               Price = c.Price,
               RentPricePerDay = c.RentPricePerDay
           })
           .ToList();

        public bool IsByDealer(int carId, int dealerId)
            => this.db
                .Cars
                .Any(x => x.Id == carId && x.DealerId == dealerId);

        public bool LocationExsts(int locationId)
         => db.Locations
            .Any(x => x.Id == locationId);
    }
}
