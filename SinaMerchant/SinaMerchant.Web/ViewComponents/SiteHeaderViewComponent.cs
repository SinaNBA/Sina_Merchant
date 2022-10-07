using Microsoft.AspNetCore.Mvc;

namespace SinaMerchant.Web.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("SiteHeader");
        }
    }
}
