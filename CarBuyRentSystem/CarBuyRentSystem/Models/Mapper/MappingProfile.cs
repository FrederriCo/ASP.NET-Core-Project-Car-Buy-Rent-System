namespace CarBuyRentSystem.Core.Mapper
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Core.Models.Users;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;
    

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetailsServiceModel, AddCarFormModel>();
            CreateMap<Car, CarListingVIewModel>();
            CreateMap<Car, CarServiceListingViewModel>();
            CreateMap<RentCar, RentedCarViewModel>();


            CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
