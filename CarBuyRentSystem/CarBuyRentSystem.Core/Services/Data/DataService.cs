namespace CarBuyRentSystem.Core.Services.Data
{

    using CarBuyRentSystem.Data;
    public class DataService
    {
        protected readonly CarDbContext db;

        public DataService(CarDbContext db)
        {
            this.db = db;
        }
    }
}
