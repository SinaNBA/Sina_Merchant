using SinaMerchant.Web.Entities;

namespace SinaMerchant.Web.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserHasPermission(int id);
    }
}
