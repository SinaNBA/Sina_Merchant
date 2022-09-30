using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Insert(TEntity entity);
        ICollection<TEntity> GetAll();
        TEntity GetById(object id);
        TEntity Filter(Expression<Func<TEntity, bool>> filterExpression);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool DeleteById(object id);


    }
}
