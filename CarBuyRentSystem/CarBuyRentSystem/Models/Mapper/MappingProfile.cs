namespace CarBuyRentSystem.Core.Mapper
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Infrastructure.Models;
    using CarBuyRentSystem.Models.Cars;
    

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetailsServiceModel, AddCarFormModel>();
            CreateMap<Car, CarListingVIewModel>();
            CreateMap<Car, CarDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
        }
    }
}
