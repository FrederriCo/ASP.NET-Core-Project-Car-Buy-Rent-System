namespace CarBuyRentSystem.Models.Mapper
{
    using AutoMapper;

    using CarBuyRentSystem.Core.Models;   
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Core.Models.View.Cars;
    using CarBuyRentSystem.Core.Models.Service.Cars;
    using CarBuyRentSystem.Core.Models.View.RentCars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetailsServiceModel, AddCarFormServiceModel>();
            CreateMap<AddMyWalletBindingModel, AddMyWalletServiceModel>();
            CreateMap<CompareCarsViewServiceModel, CompareCarsViewModel>();         
            CreateMap<Location, CarLocationServiceModel>();
            CreateMap<AddCarFormServiceModel, Car>();
            CreateMap<CarListingViewModel, Car>();
            CreateMap<RentCar, RentedCarsViewModel>();
            CreateMap<BuyCar, BuyCarBindingModel>().ReverseMap();
            CreateMap<BuyCar, SoldCarsViewModel>();
            CreateMap<RentCarBindingModel, RentCar>();
            CreateMap<DealerFormServiceModel, Dealer>();

            CreateMap<Car, CarListingViewModel>()
             .ForMember(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name));

            CreateMap<Car, CarServiceListingViewModel>()
               .ForMember(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name));

            CreateMap<Car, CarDetailsServiceModel>()
            .ForMember(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name))
            .ForMember(c => c.DealerName, cfg => cfg.MapFrom(c => c.Dealer.Name))
            .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
