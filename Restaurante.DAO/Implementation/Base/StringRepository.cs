using Restaurante.Migrations;
using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public class StringRepository<T> : IRepository<T> where T : StringEntity
    {
        private readonly RestauranteContext _ctx;

        public StringRepository(RestauranteContext ctx)
        {
            _ctx = ctx;
        }
        public void Delete(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _ctx.Update(entity);
        }

        public void Delete(IList<T> entity)
        {
            var deleteTime = DateTime.UtcNow;
            _ctx.UpdateRange(entity.Select(x => { x.DeletedAt = deleteTime; return x; }));
        }

        public void Delete(IEnumerable<T> entity)
        {
            var deleteTime = DateTime.UtcNow;
            _ctx.UpdateRange(entity.Select(x => { x.DeletedAt = deleteTime; return x; }));
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, bool>> orderByExpression = null, bool ascending = false, int take = 0)
        {
            var expression = _ctx.Set<T>().Where(whereExpression);
            if (take > 0)
                expression = expression.Take(take);

            if (orderByExpression != null)
                expression = ascending ? expression.OrderBy(orderByExpression) : expression.OrderByDescending(orderByExpression);

            return expression;
        }

        public async Task Insert(T entity)
        {
            await _ctx.AddAsync(entity);
        }

        public async Task Insert(IList<T> entity)
        {
            await _ctx.AddRangeAsync(entity);
        }

        public async Task Insert(IEnumerable<T> entity)
        {
            await _ctx.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            _ctx.Update(entity);
        }

        public void Update(IList<T> entity)
        {
            _ctx.UpdateRange(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            _ctx.UpdateRange(entity);
        }
    }
}
