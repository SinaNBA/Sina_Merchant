using Microsoft.AspNetCore.Mvc;

namespace SinaMerchant.Web.ViewComponents
{
    public class SiteFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("SiteFooter");
        }
    }
}
