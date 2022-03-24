namespace CarBuyRentSystem.Models.Cars
{
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Models.Cars.Enums;
    using System.Collections.Generic;
    
    public class AllCarsViewModel
    {
        public const int CarPerPage = 6;

        public string Brand { get; set; }

        public string Search { get; init; }

        public CarSorting Sorting { get; init; }

        public int CurentPage { get; init; } = 1;
        //public bool HasPreviusPage => this.CurentPage > 1;

        //public int PreviusPageNumber => this.CurentPage - 1;

        //public bool HasNextPage => this.CurentPage < this.PageCount;

        //public int NextPageNumber => this.CurentPage + 1;

        //public int PageCount => (int)Math.Ceiling((double)this.TotalCars / CarPerPage);

        public int TotalCars { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<CarServiceListingViewModel> Cars { get; set; }
    }
}
