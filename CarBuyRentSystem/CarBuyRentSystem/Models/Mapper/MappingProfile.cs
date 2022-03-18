namespace CarBuyRentSystem.Core.Mapper
{
    using AutoMapper;
    using CarBuyRentSystem.Core.Models.Cars;
    using CarBuyRentSystem.Models.Cars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetailsServiceModel, AddCarFormModel >();
        }
    }
}
