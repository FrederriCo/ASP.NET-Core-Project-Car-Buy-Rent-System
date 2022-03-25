namespace CarBuyRentSystem.Core.Mapper
{
    using AutoMapper;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetailsServiceModel, AddCarFormModel>();
            CreateMap<Car, Car>();
            CreateMap<Infrastructure.Models.Car, CarServiceListingViewModel>();
            CreateMap<RentCar, RentedCarsViewModel>();
            CreateMap<BuyCar, BuyCarBindingModel>().ReverseMap();
            CreateMap<BuyCar, SoldCarsViewModel>();
            CreateMap<RentCarBindingModel, RentCar > ();

            CreateMap<Infrastructure.Models.Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
