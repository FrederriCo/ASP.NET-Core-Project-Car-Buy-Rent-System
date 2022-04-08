namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using CarBuyRentSystem.Core.Models;
    using System.Threading.Tasks;
    public interface IDealerService
    {
        Task<bool> Create(DealerFormServiceModel dealer, string userId);

        Task<bool> IsDealer(string userId);

        Task<int> GetDealerId(string userId);      
    }
}
