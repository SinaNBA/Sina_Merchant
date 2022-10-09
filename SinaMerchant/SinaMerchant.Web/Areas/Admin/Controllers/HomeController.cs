using Microsoft.AspNetCore.Mvc;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
