namespace CarBuyRentSystem.Tests.Models
{ 
    using AutoMapper;
    using CarBuyRentSystem.Core.Mapper;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get 
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
