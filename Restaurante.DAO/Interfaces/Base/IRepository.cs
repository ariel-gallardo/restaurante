using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public interface IRepository<T> where T : class
    {
        Task<int> Insert(T entity);
        Task<int> Insert(IList<T> entity);
        Task<int> Insert(IEnumerable<T> entity);
        Task<int> Update(T entity);
        Task<int> Update(IList<T> entity);
        Task<int> Update(IEnumerable<T> entity);
        Task<int> Delete(T entity);
        Task<int> Delete(IList<T> entity);
        Task<int> Delete(IEnumerable<T> entity);
        IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false);
        IQueryable<T> WhereActive(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false);
        IQueryable<T> WhereSoftDeleted(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false);
        bool ExistsActive(dynamic id);
        bool ExistsSoftDeleted(dynamic id);
        Task<bool> Restore(dynamic id);
        (int,IQueryable<T>) WhereAsPaginateQuerie(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> ordenarPor = null, bool ascendente = true, int page = 1);
        Task<Paginacion<T>> WhereAsPaginateListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> ordenarPor = null, bool ascendente = true, int page = 1);
        IUnitOfWork UnitOfWork { get; set; }
    }
}
