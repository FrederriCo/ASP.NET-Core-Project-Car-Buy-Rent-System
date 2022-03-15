namespace CarBuyRentSystem.Core.Services.Dealrs
{
    public interface IDealerService
    {
        public bool IsDealer(string userId);

        public int GetDealerId(string userId);
    }
}
