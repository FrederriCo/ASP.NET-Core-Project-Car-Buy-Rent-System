namespace CarBuyRentSystem.Models.Cars
{
    using CarBuyRentSystem.Models.Cars.Enums;
    using System.Collections.Generic;
    public class AllCarsViewModel
    {
        public const int CarPerPage = 3;

        public string Brand { get; set; }

        public string Search { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurentPage { get; init; } = 1;

        public int TotalCars { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<CarListingVIewModel> Cars { get; set; }
    }
}
