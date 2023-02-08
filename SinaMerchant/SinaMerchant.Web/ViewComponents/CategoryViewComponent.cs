using Microsoft.AspNetCore.Mvc;

namespace SinaMerchant.Web.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Category");
        }
    }
}
