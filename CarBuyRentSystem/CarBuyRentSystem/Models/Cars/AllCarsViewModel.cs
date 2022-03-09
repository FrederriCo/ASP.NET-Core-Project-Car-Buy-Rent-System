namespace CarBuyRentSystem.Models.Cars
{
    using CarBuyRentSystem.Models.Cars.Enums;
    using System.Collections.Generic;
    public class AllCarsViewModel
    {
        public string Search { get; init; }
        public IEnumerable<string> Brands { get; init; }
        public CarSorting Sorting { get; init; }
        public IEnumerable<CarListingVIewModel> Cars { get; init; }
    }
}
