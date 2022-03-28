namespace CarBuyRentSystem.Core.Models.Cars
{
    using System.Collections.Generic;
    public class CarQueryServiceModel
    {
        public int CarsPerPage { get; init; }

        public int CurentPage { get; init; }

        public int TotalCars { get; set; }    
        
        public IEnumerable<CarServiceListingViewModel> Cars { get; set; }
    }
}
