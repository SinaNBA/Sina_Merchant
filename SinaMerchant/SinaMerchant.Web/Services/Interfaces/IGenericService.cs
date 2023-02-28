using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Services
{
    public interface IGenericService<TEntity, TViewModel> where TEntity : class where TViewModel : class
    {
        bool Insert(TViewModel entityModel);
        Task<ICollection<TViewModel>> GetAll();
        Task<TViewModel> GetById(object id);
        ICollection<TViewModel>? Filter(Expression<Func<TViewModel, bool>> filterExpression);
        Task<TViewModel> GetSingleNoTracking(Expression<Func<TViewModel, bool>> filterExpression);
        bool Update(TViewModel entityModel);
        bool Delete(TViewModel entityModel);
        Task<bool> DeleteById(object id);
        void Save();

    }
}
