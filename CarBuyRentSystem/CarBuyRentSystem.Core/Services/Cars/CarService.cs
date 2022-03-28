namespace CarBuyRentSystem.Core.Services.Cars
{
    using System;
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Infrastructure.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;

    public class CarService : DataService, ICarService
    {
        private readonly IConfigurationProvider mapper;

        public CarService(CarDbContext db, IConfigurationProvider mapper)
            : base(db)
        {
            this.mapper = mapper;
        }

        public async Task Add(Car car)
        {
            await this.db.Cars.AddAsync(car);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarServiceListingViewModel>> AdminGetAllCar()
             => await db.Cars
               .ProjectTo<CarServiceListingViewModel>(this.mapper)
               .ToListAsync();


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

        public async Task<IEnumerable<string>> AllCarBrands()
           => await db
              .Cars
              .Select(c => c.Brand)
              .Distinct()
              .OrderBy(b => b)
              .ToListAsync();

        public async Task<IEnumerable<CarLocationServiceModel>> AllCarLocation()
            => await  this.db
            .Locations
            .ProjectTo<CarLocationServiceModel>(this.mapper)
            //.Select(l => new CarLocationServiceModel
            //{
            //    Id = l.Id,
            //    Name = l.Name
            //})
            .ToListAsync();

        public async Task<bool> Buy(BuyCar buyCar, string username)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.db.Cars.SingleOrDefaultAsync(c => c.Id == buyCar.CarId);

            if (user == null || car == null /*|| user.Balance < car.Price*/ )
            {
                return false;
            }

            buyCar.User = user;
            buyCar.BoughtOn = DateTime.Now;
            buyCar.Price = car.Price;

            //user.Balance -= car.Price;

            this.db.BuyCars.Add(buyCar);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CarServiceListingViewModel>> ByUser(string userId)
            => await this.GetCars(this.db
                         .Cars
                         .Where(c => c.Dealer.UserId == userId));

        public async Task ChangeVisability(int carId)
        {
            var car = db.Cars.Find(carId);

            car.IsPublic = !car.IsPublic;

            await db.SaveChangesAsync();
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
        public async Task Delete(int id)
        {
            var car = await db.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return;
            }

            db.Cars.Remove(car);
            await db.SaveChangesAsync();
        }

        public async Task<CarDetailsServiceModel> Details(int id)
            => await this.db.Cars
                    .Where(c => c.Id == id)
                    .ProjectTo<CarDetailsServiceModel>(this.mapper)
                    .FirstOrDefaultAsync();
        //--With AutoMapper
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


        public async Task Edit(CreateCarServiceModel car)
        {
            var carData = await this.db.Cars.FindAsync(car.Id);

            if (carData == null)
            {
                return;
            }

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

            await db.SaveChangesAsync();

        }

        public async Task<Car> GetCarId(int id)
        {
            var car = await db
                           .Cars
                           .FirstOrDefaultAsync(x => x.Id == id);

            return car;
        }

        public async Task<IEnumerable<CarServiceListingViewModel>> GetCars(IQueryable<Car> carQuery)
         => await carQuery
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
           .ToListAsync();

        public async Task<IEnumerable<CarListingVIewModel>> GetLastThreeCar()
        {
            var cars = await db
              .Cars
              .OrderByDescending(c => c.Id)
              .ProjectTo<CarListingVIewModel>(this.mapper)
              .Take(3)
              .ToListAsync();

            return cars;
        }

        public async Task<bool> IsByDealer(int carId, int dealerId)
            => await this.db
                .Cars
                .AnyAsync(x => x.Id == carId && x.DealerId == dealerId);

        public async Task<bool> LocationExists(int locationId)
            => await db.Locations
            .AnyAsync(x => x.Id == locationId);

        public async Task<bool> Rent(RentCar rentCar, string username)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.db.Cars.SingleOrDefaultAsync(c => c.Id == rentCar.CarId);

            if (user == null || car == null)
            {
                return false;
            }

            rentCar.StartDate = DateTime.Now;
            rentCar.CarUser = user;

            var totalDays = (rentCar.EndDate - rentCar.StartDate).Days + 1;

            rentCar.TotalPrice = totalDays * car.RentPricePerDay;

            //user.Balance -= rentCar.TotalPrice;

            //if (user.Balance < rentCar.TotalPrice)
            //{
            //    return false;
            //}

            this.db.RentCars.Add(rentCar);

            await this.db.SaveChangesAsync();

            return true;
        }

        public TotalUserCar Total()
        {
            var cars = db.Cars.Count();
            var dealer = db.Dealers.Count();
            var users = db.Users.Count();
            //var rentCars = db.RentCars.Count();
            //var buyCars = db.RentCars.Count();

            var carsUser = new TotalUserCar
            {
                TotalCar = cars,
                TotalUser = users,
                TotalDealer = dealer,
            };

            return carsUser;
        }


    }
}