namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using System.Threading.Tasks;
    public interface IDealerService
    {
        Task<bool> IsDealer(string userId);

        Task<int> GetDealerId(string userId);

        Task<int> TotalUser();
    }
}
