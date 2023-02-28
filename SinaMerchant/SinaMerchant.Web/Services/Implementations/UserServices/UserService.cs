using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;
using SinaMerchant.Web.Repositories;
using SinaMerchant.Web.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Services
{
    public class UserService<TModel> : IUserService<TModel> where TModel : class
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool Insert(TModel model)
        {
            var entity = _mapper.Map<User>(model);
            return _repository.Insert(entity);
        }

        public async Task<ICollection<TModel>> GetAll()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<ICollection<TModel>>(list);
        }

        public async Task<TModel> GetById(object id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<TModel>(entity);
        }

        public ICollection<TModel>? Filter(Expression<Func<TModel, bool>> filterExpression)
        {
            var entityFilterExpression = _mapper.Map<Expression<Func<User, bool>>>(filterExpression);
            var list = _repository.Filter(entityFilterExpression);
            return _mapper.Map<ICollection<TModel>>(list);
        }

        public async Task<TModel> GetSingleNoTracking(Expression<Func<TModel, bool>> filterExpression)
        {
            var entityFilterExpression = _mapper.Map<Expression<Func<User, bool>>>(filterExpression);
            var entity = await _repository.GetSingleNoTracking(entityFilterExpression);
            return _mapper.Map<TModel>(entity);
        }

        public async Task<bool> IsExist(Expression<Func<TModel, bool>> filterExpression)
        {
            if ((await GetSingleNoTracking(filterExpression)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(TModel model)
        {
            var entity = _mapper.Map<User>(model);
            return _repository.Update(entity);
        }

        public bool Delete(TModel model)
        {
            var entity = _mapper.Map<User>(model);
            return _repository.Delete(entity);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await _repository.DeleteById(id);
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public bool UserHasPermission(int id)
        {
            return _repository.UserHasPermission(id);
        }
    }
}
