namespace CarBuyRentSystem.Core.Services.DataService
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
