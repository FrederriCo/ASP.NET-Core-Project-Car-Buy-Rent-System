namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using CarBuyRentSystem.Core.Services.Cars;
    using CarBuyRentSystem.Core.Services.UserService;
    using CarBuyRentSystem.Core.Models.View.RentCars;   
  
    public class CarsController : AdminController
    {
        private readonly ICarService carService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

       
        public CarsController(ICarService carService
                              ,IUserService userService
                             ,IMapper mapper)                              
        {
            this.carService = carService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var total = carService.Total();

             return View(total);
        }

        public async Task<IActionResult> All()
        {
             return View(await this.carService.AdminGetAllCar());            
        }

        public async Task<IActionResult> Edit()
        { 
             return View(await this.carService.AdminGetAllCar());       
        }

        public async Task<IActionResult> Delete()
        {
            return View(await this.carService.AdminGetAllCar());
        }

        public async Task<IActionResult> AllCars()
        {
            return View(await this.carService.AdminGetAllCar());
        }

        public async Task<IActionResult> ChangeVisability(int id)
        {
           await carService.ChangeVisability(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> RentedCars()
        {
            var rentedCars = (await this.userService.GetAllRentedCars())
                .Select(mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCars);
        }

        public async Task<IActionResult> SoldCars()
        {
            var soldCars = (await this.userService.GetAllSoldCars())
                .Select(mapper.Map<SoldCarsViewModel>);

            return this.View(soldCars);
        }
    }
}
