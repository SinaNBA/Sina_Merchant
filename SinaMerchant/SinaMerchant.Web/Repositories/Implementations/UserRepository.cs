using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public class UserRepository : ShopRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool UserHasPermission(int id)
        {
            var user = _context.Set<User>()
               .Include(u => u.UserRoles)
               .ThenInclude(ur => ur.Role)
               .ThenInclude(r => r.RolePermissions)
               .ThenInclude(rp => rp.Permission)
               .SingleOrDefault(x => x.Id == id);

            var hasPermission = user.UserRoles.Any(ur => ur.Role.RolePermissions.Any());
            if (hasPermission || user.IsAdmin) return true;
            else return false;

        }
    }
}
