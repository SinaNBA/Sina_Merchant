using Microsoft.AspNetCore.Mvc;
using SinaMerchant.Web.HttpExtentions;

namespace SinaMerchant.Web.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
         {
            //TODO check if it has permission
            ViewBag.hasAccess = HasPermission();
            return View("SiteHeader");
        }

        [PermissionChecker("AdminDashboard")]
        public bool HasPermission()
        {
            return true;
        }
    }
}
