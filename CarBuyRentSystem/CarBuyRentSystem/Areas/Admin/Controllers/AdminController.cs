namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using static CarBuyRentSystem.Infrastructure.Data.WebConstants;

    [Area(AdministratorAreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }    
}
