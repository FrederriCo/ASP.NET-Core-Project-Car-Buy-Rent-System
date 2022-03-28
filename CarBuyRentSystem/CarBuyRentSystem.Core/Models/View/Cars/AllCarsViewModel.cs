namespace CarBuyRentSystem.Core.Models.View.Cars
{
    using System.Collections.Generic;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars.Enums;
    
    public class AllCarsViewModel
    {
        public const int CarPerPage = 3;

        public string Brand { get; set; }

        public string Search { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurentPage { get; init; } = 1;        

        public int TotalCars { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<CarServiceListingViewModel> Cars { get; set; }
    }
}
