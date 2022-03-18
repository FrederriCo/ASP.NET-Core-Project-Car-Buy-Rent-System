﻿namespace CarBuyRentSystem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = WebConstants.AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}