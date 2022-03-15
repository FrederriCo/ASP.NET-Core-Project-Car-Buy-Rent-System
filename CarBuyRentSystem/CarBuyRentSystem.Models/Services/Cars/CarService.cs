namespace CarBuyRentSystem.Core.Services.Cars
{
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Data;
    using CarBuyRentSystem.Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class CarService : ICarService
    {
        private readonly CarDbContext db;

        public CarService(CarDbContext db)
           => this.db = db;

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
    }
}
