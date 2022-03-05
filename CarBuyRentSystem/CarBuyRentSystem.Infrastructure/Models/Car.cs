﻿namespace CarBuyRentSystem.Infrastructure.Models
{
    using CarBuyRentSystem.Infrastructure.Models.Enums;
    using System.Collections.Generic;
    public class Car
    {
        public int Id { get; init; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public Fuel Fuel { get; set; }

        public Transmission Transmission { get; set; }

        public int Lugage { get; set; }

        public int Doors { get; set; }

        public int Passager { get; set; }

        public decimal RentPricePerDay { get; set; }

        public decimal Price { get; set; }

        public ICollection<BuyCar> Owners { get; set; }

        public ICollection<RentCar> Renters { get; set; }

    }
}
