
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;

namespace SinaMerchant.Web.Services
{
    public class UserHasPermission
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _context;

        public UserHasPermission(int userId, ApplicationDbContext context)
        {
            _userId = userId;
            _context = context;
        }

        public bool HasPermission()
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .SingleOrDefault(x => x.Id == _userId);

            var hasPermission = user.UserRoles.Any(ur => ur.Role.RolePermissions.Any());

            if (user.IsAdmin || hasPermission)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

