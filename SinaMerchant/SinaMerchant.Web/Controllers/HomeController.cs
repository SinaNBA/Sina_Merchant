using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinaMerchant.Web.HttpExtentions;
using SinaMerchant.Web.Models;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;
using System.Diagnostics;

namespace SinaMerchant.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService<UserAddEditViewModel> _userService;

        public HomeController(ILogger<HomeController> logger, IUserService<UserAddEditViewModel> userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.User.GetCurrentUserId();
            ViewBag.HasPermission = _userService.UserHasPermission(userId);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}