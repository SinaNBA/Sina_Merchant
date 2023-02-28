using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Repositories;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Services
{
    public interface IUserService<TModel> where TModel : class
    {
        bool Insert(TModel model);

        Task<ICollection<TModel>> GetAll();
        Task<TModel> GetById(object id);
        ICollection<TModel>? Filter(Expression<Func<TModel, bool>> filterExpression);
        Task<TModel> GetSingleNoTracking(Expression<Func<TModel, bool>> filterExpression);
        Task<bool> IsExist(Expression<Func<TModel, bool>> filterExpression);
        bool UserHasPermission(int id);

        bool Update(TModel model);
        bool Delete(TModel model);
        Task<bool> DeleteById(object id);
        void Save();

    }
}
