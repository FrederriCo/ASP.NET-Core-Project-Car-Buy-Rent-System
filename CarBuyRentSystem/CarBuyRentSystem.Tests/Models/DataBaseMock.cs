namespace CarBuyRentSystem.Tests.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using CarBuyRentSystem.Infrastructure.Data;

    public class DataBaseMock
    {
        public static CarDbContext Instane
        {
            get
            {
                var dbContextOption = new DbContextOptionsBuilder<CarDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                    .Options;
                return new CarDbContext(dbContextOption);
            }
        }
    }
}
