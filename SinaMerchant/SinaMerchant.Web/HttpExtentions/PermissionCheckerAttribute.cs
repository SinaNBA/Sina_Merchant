using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;

namespace SinaMerchant.Web.HttpExtentions
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public PermissionCheckerAttribute(string permission)
        {
            _permission = permission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetCurrentUserId();
                var _context = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
                var user = _context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .SingleOrDefault(x => x.Id == userId);

                var hasPermission = user.UserRoles.Any(u => u.Role.RolePermissions.Any(p => p.Permission.Name == _permission));

                if (user.IsAdmin||hasPermission)
                {
                    // log admin activity
                }
                else
                {
                    context.Result = new RedirectResult("/");
                }
            }
            else
            {
                context.Result = new RedirectResult("/");
            }
        }
    }
}
