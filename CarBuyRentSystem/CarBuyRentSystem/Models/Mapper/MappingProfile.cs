namespace CarBuyRentSystem.Models.Mapper
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
            CreateMap<Car, CarListingViewModel>()                
               .ForPath(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name));

           // CreateMap<Car, CarDetailsServiceModel>();
              // .ForPath(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name))
                //.ForMember(c => c.DealerId, x => x.MapFrom(y => y.Dealer.Id))
                //.ForMember(c => c.DealerName, x => x.MapFrom(y => y.Dealer.Name));


            CreateMap<CarDetailsServiceModel, AddCarFormServiceModel>();
            CreateMap<Location, CarLocationServiceModel>();
            CreateMap<AddCarFormServiceModel, Car>();
            CreateMap<CarListingViewModel, Car>();
            CreateMap<Car, CarServiceListingViewModel>()
                .ForPath(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name));
            CreateMap<RentCar, RentedCarsViewModel>();
            CreateMap<BuyCar, BuyCarBindingModel>().ReverseMap();
            CreateMap<BuyCar, SoldCarsViewModel>();
            CreateMap<RentCarBindingModel, RentCar>();
            CreateMap<DealerFormServiceModel, Dealer>();

            CreateMap<Car, CarDetailsServiceModel>()
            .ForPath(c => c.LocationName, cfg => cfg.MapFrom(c => c.Location.Name))
            .ForPath(c => c.DealerName, cfg => cfg.MapFrom(c => c.Dealer.Name))
            .ForPath(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
