using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public BaseRepository(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Delete(T entity)
        => _repository.Delete(entity);
        

        public void Delete(IList<T> entity)
        => _repository.Delete(entity);
        

        public void Delete(IEnumerable<T> entity)
        => _repository.Delete(entity);
        

        public async Task Insert(T entity)
        => await _repository.Insert(entity);
        

        public async Task Insert(IList<T> entity)
        => await _repository.Insert(entity);
        

        public async Task Insert(IEnumerable<T> entity)
        => await _repository.Insert(entity);
        

        public void Update(T entity)
        => _repository.Update(entity);
        

        public void Update(IList<T> entity)
        => _repository.Update(entity);
        

        public void Update(IEnumerable<T> entity)
        => _repository.Update(entity);
        

        public IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, bool>> orderByExpression = null, bool ascending = false, int take = 0)
        => _repository.Where(whereExpression, orderByExpression, ascending, take);
    }
}
