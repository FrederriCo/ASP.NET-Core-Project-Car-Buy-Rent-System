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
    using CarBuyRentSystem.Core.Models.View.Cars.Enums;

    using static Infrastructure.Data.WebConstants;
    using CarBuyRentSystem.Core.Models.Service.Cars;   

    public class CarService : DataService, ICarService
    {
        private readonly IConfigurationProvider mapper;

        public CarService(CarDbContext db, IMapper mapper)
            : base(db)
        {
            this.mapper = mapper.ConfigurationProvider;
        }

        public async Task Add(Car car)
        {
            await this.db.Cars.AddAsync(car);
            await this.db.SaveChangesAsync();
        }

        public async Task AddBalance(AddMyWalletServiceModel model, string username)
        {
            var user = await this.db.CarUsers.SingleOrDefaultAsync(u => u.UserName == username);

            user.Balance += model.Balance;

            this.db.Update(user);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarServiceListingViewModel>> AdminGetAllCar()
             => await db.Cars
               .ProjectTo<CarServiceListingViewModel>(this.mapper)
               .ToListAsync();

        public async Task<CarQueryServiceModel> All(string brand = null,
                            string search = null,
                            CarSorting sorting = CarSorting.DateCreated,
                            int currentPage = CurentPageStartValue,
                            int carsPeerPage = int.MaxValue,
                            bool publicOnly = true)
        {
            var carsQuery = this.db
                                .Cars
                                .Where(c => !publicOnly || c.IsPublic);

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                carsQuery = carsQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(search.ToLower()) ||
                    c.Description.ToLower().Contains(search.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = await GetCars(carsQuery
                       .Skip((currentPage - 1) * carsPeerPage)
                       .Take(carsPeerPage));

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurentPage = currentPage,
                CarsPerPage = carsPeerPage,
                Cars = cars
            };
        }

        public async Task<IEnumerable<string>> AllCarBrands()
               => await db
                  .Cars
                  .Select(c => c.Brand)
                  .Distinct()
                  .OrderBy(b => b)
                  .ToListAsync();

        public async Task<IEnumerable<CarLocationServiceModel>> AllCarLocation()
            => await this.db
            .Locations
            .ProjectTo<CarLocationServiceModel>(this.mapper)
            .ToListAsync();

        public async Task<bool> Buy(BuyCar buyCar, string username)
        {
            var user = await this.db
                .Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.db
                .Cars.SingleOrDefaultAsync(c => c.Id == buyCar.CarId);

            if (user == null || car == null || user.Balance < car.Price )
            {
                return false;
            }

            buyCar.User = user;
            buyCar.BoughtOn = DateTime.Now;
            buyCar.Price = car.Price;

            user.Balance -= car.Price;

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

        public async Task<CompareCarsViewServiceModel> CompareCars(int firstCarId, int secondCarId)
        {
            var firstCar = await this.db.Cars.FirstOrDefaultAsync(c => c.Id == firstCarId);

            var secondCar = await this.db.Cars.FirstOrDefaultAsync(c => c.Id == secondCarId);

            return new CompareCarsViewServiceModel
            {
                FirstCar = firstCar,
                SecondCar = secondCar
            };
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

        public async Task<CarDetailsServiceModel> GetCarId(int id)
            => await db
                    .Cars
                    .ProjectTo<CarDetailsServiceModel>(this.mapper)
                    .FirstOrDefaultAsync(x => x.Id == id);
       

        public async Task<IEnumerable<CarServiceListingViewModel>> GetCars(IQueryable<Car> carQuery)
          => await carQuery
            .ProjectTo<CarServiceListingViewModel>(this.mapper)
            .ToListAsync();

        public async Task<IEnumerable<CarListingViewModel>> GetLastThreeCar()
            => await this.db
                     .Cars
                     .Where(c => c.IsPublic)
                     .OrderByDescending(c => c.Id)
                     .ProjectTo<CarListingViewModel>(this.mapper)
                     .Take(3)
                     .ToListAsync();       
       

        public async Task<bool> IsByDealer(int carId, int dealerId)
            => await this.db
                .Cars
                .AnyAsync(x => x.Id == carId && x.DealerId == dealerId);

        public async Task<bool> LocationExists(int locationId)
            => await db.Locations
                .AnyAsync(x => x.Id == locationId);

        public async Task<bool> Rent(RentCar rentCar, string username)
        {
            var user = await this.db
                .CarUsers.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.db
                .Cars.SingleOrDefaultAsync(c => c.Id == rentCar.CarId);

            if (user == null || car == null)
            {
                return false;
            }

            rentCar.StartDate = DateTime.Now;
            rentCar.CarUser = user;

            var totalDays = (rentCar.EndDate - rentCar.StartDate).Days + 1;

            rentCar.TotalPrice = totalDays * car.RentPricePerDay;

            user.Balance -= rentCar.TotalPrice;

            if (user.Balance < rentCar.TotalPrice)
            {
                return false;
            }

            this.db.RentCars.Add(rentCar);

            await this.db.SaveChangesAsync();

            return true;
        }

        public TotalUserCar Total()
        {
            var cars = db.Cars.Count();
            var dealer = db.Dealers.Count();
            var users = db.Users.Count();
            var rentCars = db.RentCars.Count();
            var buyCars = db.BuyCars.Count();

            var carsUser = new TotalUserCar
            {
                TotalCar = cars,
                TotalUser = users,
                TotalDealer = dealer,
                TotalRentCars = rentCars,
                TotalSoldCars = buyCars
            };

            return carsUser;
        }
    }
}