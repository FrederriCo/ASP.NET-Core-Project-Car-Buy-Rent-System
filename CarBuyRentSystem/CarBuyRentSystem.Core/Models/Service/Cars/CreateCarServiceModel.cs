namespace CarBuyRentSystem.Core.Models.Cars
{
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using System.Collections.Generic;

    public class CreateCarServiceModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public Category Category { get; set; }

        public Fuel Fuel { get; set; }

        public Transmission Transmission { get; set; }

        public string ImageUrl { get; set; }

        public int Lugage { get; set; }

        public int Doors { get; set; }

        public int Passager { get; set; }

        public int LocationId { get; set; }

        public int DealerId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal RentPricePerDay { get; set; }

        public IEnumerable<CarLocationServiceModel> Locations { get; set; }
    }
}
