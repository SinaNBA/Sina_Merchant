using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _user;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _user = _context.Set<User>();
        }

        public bool Insert(User entity)
        {
            _user.AddAsync(entity);
            Save();
            return true;
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _user.ToListAsync();
        }

        public async Task<User> GetById(object id)
        {
            return await _user.FindAsync(id);
        }

        public ICollection<User>? Filter(Expression<Func<User, bool>> filterExpression)
        {

            return _user.Where(filterExpression).ToList();

        }

        public async Task<User> GetSingleNoTracking(Expression<Func<User, bool>> filterExpression)
        {
            return await _user.Where(filterExpression).AsNoTracking().SingleOrDefaultAsync();
        }

        public bool Update(User entity)
        {
            try
            {
                _context.Attach(entity);

                _context.Entry(entity).State = EntityState.Modified;
                Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public bool Delete(User entity)
        {
            try
            {
                _user.Remove(entity);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public async Task<bool> DeleteById(object id)
        {
            var entity = await GetById(id);
            return Delete(entity);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool UserHasPermission(int id)
        {
            var user = _user
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
