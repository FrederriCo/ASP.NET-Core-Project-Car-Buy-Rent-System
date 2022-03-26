namespace CarBuyRentSystem.Core.Services.Dealrs
{
    public interface IDealerService
    {
         bool IsDealer(string userId);

         int GetDealerId(string userId);

        void TotalUser();
    }
}
