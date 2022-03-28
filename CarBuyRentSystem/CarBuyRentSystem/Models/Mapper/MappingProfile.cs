namespace CarBuyRentSystem.Core.Mapper
{
    using AutoMapper;

    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;
    using CarBuyRentSystem.Core.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetailsServiceModel, AddCarFormServiceModel>();
            CreateMap<Car, Car>();
            CreateMap<AddCarFormServiceModel, Car> ();           
            CreateMap<Car, CarListingVIewModel>();
            CreateMap<Car, CarServiceListingViewModel>();
            CreateMap<RentCar, RentedCarsViewModel>();
            CreateMap<BuyCar, BuyCarBindingModel>().ReverseMap();
            CreateMap<BuyCar, SoldCarsViewModel>();
            CreateMap<RentCarBindingModel, RentCar>();
            CreateMap<DealerFormServiceModel, Dealer>();


            CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
