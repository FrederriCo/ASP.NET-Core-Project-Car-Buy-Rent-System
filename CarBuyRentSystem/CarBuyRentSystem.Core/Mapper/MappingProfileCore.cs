//using AutoMapper;
//using CarBuyRentSystem.Core.Models;
//using CarBuyRentSystem.Core.Models.Cars;
//using CarBuyRentSystem.Core.Models.View.Cars;
//using CarBuyRentSystem.Core.Models.View.RentCars;
//using CarBuyRentSystem.Infrastructure.Models;


//namespace CarBuyRentSystem.Core.Mapper
//{
//    public class MappingProfileCore : Profile
//    {
//        public MappingProfileCore()
//        {
//            CreateMap<CarDetailsServiceModel, AddCarFormServiceModel>();
//            CreateMap<Car, Car>();
//            CreateMap<Car, CarListingVIewModel>();
//            CreateMap<Car, CarServiceListingViewModel>();
//            CreateMap<RentCar, RentedCarsViewModel>();
//            CreateMap<BuyCar, BuyCarBindingModel>().ReverseMap();
//            CreateMap<BuyCar, SoldCarsViewModel>();
//            CreateMap<RentCarBindingModel, RentCar>();
//            CreateMap<DealerFormServiceModel, Dealer>();


//            CreateMap<Car, CarDetailsServiceModel>()
//                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));
//        }
//    }
//}
