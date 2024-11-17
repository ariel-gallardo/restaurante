using Microsoft.EntityFrameworkCore;
using Restaurante.Migrations;
using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public class BigIntRepository<T> : IRepository<T> where T : BigIntEntity
    {
        private readonly RestauranteContext _ctx;

        public IUnitOfWork UnitOfWork { get; set; }

        public BigIntRepository(RestauranteContext ctx)
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

        public IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false, int take = 0)
        {
            var expression = _ctx.Set<T>().Where(whereExpression);
            if(take > 0)
                expression = expression.Take(take);

            if(orderByExpression != null)
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

        public IQueryable<T> WhereActive(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false, int take = 0)
        {
            var activeExpression = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(Expression.Parameter(typeof(T), "x"), "DeletedAt"),
                    Expression.Constant(null, typeof(DateTime?))
                ),
                Expression.Parameter(typeof(T), "x")
            );

            var combinedLambdaExpression = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(whereExpression, activeExpression),
                whereExpression.Parameters
            );

            return Where(combinedLambdaExpression, orderByExpression, ascending, take);
        }

        public IQueryable<T> WhereSoftDeleted(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false, int take = 0)
        {
            var activeExpression = Expression.Lambda<Func<T, bool>>(
                Expression.NotEqual(
                    Expression.Property(Expression.Parameter(typeof(T), "x"), "DeletedAt"),
                    Expression.Constant(null, typeof(DateTime?))
                ),
                Expression.Parameter(typeof(T), "x")
            );

            var combinedLambdaExpression = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(whereExpression, activeExpression),
                whereExpression.Parameters
            );

            return Where(combinedLambdaExpression, orderByExpression, ascending, take);
        }
        public bool ExistsActive(dynamic id)
        {
            long cId = 0L;
            if(id != null) long.TryParse(id, out cId);
            return cId > 0 ? WhereActive(x => x.Id == cId).Take(1).Count() == 1 : false;
        }
        public bool ExistsSoftDeleted(dynamic id)
        {
            long cId = 0L;
            if (id != null) long.TryParse(id, out cId);
            return cId > 0L ? WhereSoftDeleted(x => x.Id == cId).Take(1).Count() == 1 : false;
        }

        public async Task<bool> Restore(dynamic id)
        {
            long cId = 0L;
            if (id != null) long.TryParse(id, out cId);
            var entity = cId > 0 ? await WhereSoftDeleted(x => x.Id == cId).FirstOrDefaultAsync() : null;
            if (entity == null) return false;
            entity.DeletedAt = null;
            Update(entity);
            return true;
        }
    }
}
