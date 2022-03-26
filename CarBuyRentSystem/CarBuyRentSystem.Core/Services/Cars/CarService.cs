namespace CarBuyRentSystem.Core.Services.Cars
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Services.Data;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarService : DataService, ICarService
    {       
        private readonly IConfigurationProvider mapper;

        public CarService(CarDbContext db, IConfigurationProvider mapper)
            : base(db)
        {
            
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

        public void Delete(int id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return;
            }

            db.Cars.Remove(car);
            db.SaveChanges();
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

        public Car GetCarId(int id)
        {
            var car = db.Cars.FirstOrDefault(x => x.Id == id);

            return car;
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

        public bool IsByDealer(int carId, int dealerId)
            => this.db
                .Cars
                .Any(x => x.Id == carId && x.DealerId == dealerId);

        public bool LocationExsts(int locationId)
         => db.Locations
            .Any(x => x.Id == locationId);

        public async Task<bool> Rent(RentCar rentCar, string username)
        {
            var user = await this.db.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.db.Cars.SingleOrDefaultAsync(c => c.Id == rentCar.CarId);

            if (user == null || car == null)
            {
                return false;
            }

            rentCar.StartDate = DateTime.Now;
            rentCar.CarUser  = user;

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

            var carsUser = new TotalUserCar
            {
                TotalCar = cars,
                TotalUser = users,
                TotalDealer = dealer
            };

            return carsUser;
        }

       
    }
}
