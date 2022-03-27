namespace CarBuyRentSystem.Core.Services.Dealrs
{
    using CarBuyRentSystem.Core.Models;
    using System.Threading.Tasks;
    public interface IDealerService
    {
        bool Create(DealerFormServiceModel dealer, string userId);

        bool IsDealer(string userId);

        int GetDealerId(string userId);

        void TotalUser();
    }
}
