using AutoMapper;
using CarBuyRentSystem.Controllers;
using CarBuyRentSystem.Core.Models;
using CarBuyRentSystem.Core.Services.Dealrs;
using CarBuyRentSystem.Infrastructure.Data;
using CarBuyRentSystem.Infrastructure.Models;
using CarBuyRentSystem.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarBuyRentSystem.Tests.Controllers
{
    public class DealerTest
    {
         private IMapper mapper = MapperMock.Instance;
        private const string userId = "TestUserId";

        [Fact]
        public async Task IsDealer()
        {
            var data = GetDealerData();

            var dealarService = new DealerService(data, mapper);

            var result = await dealarService.IsDealer(userId);

            Assert.True(result);
            Assert.Equal(1, data.Dealers.Count());
        }

        [Fact]
        public async Task IsDealerNotDealer()
        {
            //Arrange
            using var data = GetDealerData();

            var dealarService = new DealerService(data, mapper);

            //Act
            var result = await dealarService.IsDealer("Anathoer User");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void DealerControllerCreateView()
        {
            var dealerController = new DealersController(Mock.Of<IDealerService>());

            var result = dealerController.Create();

            Assert.NotNull(result);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DealerControllerCreateViewOne()
        {
            var dealerController = new DealersController(Mock.Of<IDealerService>());
            var dealerCreate = new DealerFormServiceModel { Name = "Peshi", PhoneNumber = "9999" };

            using var data = GetDealerData();

            var dealarService = new DealerService(data, mapper);

            var dataa = await dealarService.Create(Mock.Of<DealerFormServiceModel>(), userId);

            ;

            // Assert.NotNull(dataa);

            Assert.False(dataa);
        }

        private CarDbContext GetDealerData()
        {
            var data = DataBaseMock.Instane;

            data.Dealers.Add(new Dealer { UserId = userId });
            data.SaveChanges();

            return data;
        }

    }
}
