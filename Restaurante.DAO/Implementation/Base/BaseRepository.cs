using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public IUnitOfWork UnitOfWork { get; set; }
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

        public bool ExistsActive(dynamic id)
        => _repository.ExistsActive(id);
        public bool ExistsSoftDeleted(dynamic id)
        => _repository.ExistsSoftDeleted(id);

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
        

        public IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        => _repository.Where(whereExpression, orderByExpression, ascending);

        public IQueryable<T> WhereActive(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        => _repository.WhereActive(whereExpression, orderByExpression, ascending);

        public IQueryable<T> WhereSoftDeleted(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        => _repository.WhereSoftDeleted(whereExpression, orderByExpression, ascending);

        public async Task<bool> Restore(dynamic id)
        => await _repository.Restore(id);

        public async Task<Paginacion<T>> WhereAsPaginateListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> ordenarPor = null, bool ascendente = true, int page = 1)
        => await _repository.WhereAsPaginateListAsync(whereExpression,ordenarPor, ascendente, page);

        public (int,IQueryable<T>) WhereAsPaginateQuerie(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> ordenarPor = null, bool ascendente = true, int page = 1)
        => _repository.WhereAsPaginateQuerie(whereExpression, ordenarPor, ascendente, page);

        public async Task<bool> Delete(dynamic id)
        => await _repository.Delete(id);
    }
}
