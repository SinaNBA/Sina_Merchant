using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Services;
using System.Security.Claims;

namespace SinaMerchant.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IGenericService<User, UserAddEditViewModel> _userService;

        public HomeController(IGenericService<User, UserAddEditViewModel> userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentUser = await _userService.GetById(id);
            return View(currentUser);
        }
    }
}
