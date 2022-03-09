using CarBuyRentSystem.Infrastructure.Models.Enums;

namespace CarBuyRentSystem.Models.Cars
{
    public class CarListingVIewModel
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

        public string Locaton { get; set; }

        public decimal Price { get; set; }

        public decimal RentPricePerDay { get; set; }

    }
}
