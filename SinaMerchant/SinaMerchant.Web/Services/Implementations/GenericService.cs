using AutoMapper;
using SinaMerchant.Web.Repositories;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Services
{
    public class GenericService<TEntity, TViewModel> : IGenericService<TEntity, TViewModel>
        where TEntity : class where TViewModel : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool Insert(TViewModel entityModel)
        {
            var entity = _mapper.Map<TEntity>(entityModel);
            return _repository.Insert(entity);
        }

        public async Task<ICollection<TViewModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<ICollection<TViewModel>>(entities);
        }

        public async Task<TViewModel> GetById(object id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<TViewModel>(entity);
        }

        public ICollection<TViewModel>? Filter(Expression<Func<TViewModel, bool>> filterExpression)
        {
            var entityFilterExpression = _mapper.Map<Expression<Func<TEntity, bool>>>(filterExpression);
            var list = _repository.Filter(entityFilterExpression);
            return _mapper.Map<ICollection<TViewModel>>(list);
        }

        public async Task<TViewModel> GetSingleNoTracking(Expression<Func<TViewModel, bool>> filterExpression)
        {
            var entityFilterExpression = _mapper.Map<Expression<Func<TEntity, bool>>>(filterExpression);
            var entity = await _repository.GetSingleNoTracking(entityFilterExpression);
            return _mapper.Map<TViewModel>(entity);
        }

        public bool Update(TViewModel entityModel)
        {
            var entity = _mapper.Map<TEntity>(entityModel);
            return _repository.Update(entity);
        }

        public bool Delete(TViewModel entityModel)
        {
            var entity = _mapper.Map<TEntity>(entityModel);
            return _repository.Delete(entity);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await _repository.DeleteById(id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
