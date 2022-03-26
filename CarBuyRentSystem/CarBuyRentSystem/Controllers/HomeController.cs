namespace CarBuyRentSystem.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using CarBuyRentSystem.Models;
    using Microsoft.AspNetCore.Mvc;

    using CarBuyRentSystem.Core.Services.Cars;

    public class HomeController : Controller
    {
        private readonly ICarService carService;        

        public HomeController(ICarService carService)
        {
           this.carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await carService.GetLastThreeCar();

            return View(cars);
        }

        public IActionResult ApplicationError()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });       
        
    }
}
