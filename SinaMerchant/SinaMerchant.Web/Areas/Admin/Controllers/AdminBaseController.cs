using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinaMerchant.Web.HttpExtentions;

namespace SinaMerchant.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker("AdminDashboard")]
    public class AdminBaseController : Controller { }

}
