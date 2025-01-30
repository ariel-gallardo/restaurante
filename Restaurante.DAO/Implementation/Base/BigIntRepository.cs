using Microsoft.EntityFrameworkCore;
using Restaurante.Infraestructure;
using Restaurante.Migrations;
using Restaurante.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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
        public async Task<int> Delete(T entity)
        {
            _ctx.Remove(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> Delete(IList<T> entity)
        {
            if (entity?.Count > 0)
            {
                _ctx.RemoveRange(entity);
                return await _ctx.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(IEnumerable<T> entity)
        {
            if (entity?.Count() > 0)
            {
                _ctx.RemoveRange(entity);
                return await _ctx.SaveChangesAsync();
            }
            return 0;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        {
            var expression = _ctx.Set<T>().Where(whereExpression);

            if(orderByExpression != null)
            expression = ascending ? expression.OrderBy(orderByExpression) : expression.OrderByDescending(orderByExpression);

            return expression;
        }

        public async Task<int> Insert(T entity)
        {
            await _ctx.AddAsync(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> Insert(IList<T> entity)
        {
            if (entity?.Count > 0)
            {
                await _ctx.AddRangeAsync(entity);
                return await _ctx.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Insert(IEnumerable<T> entity)
        {
            if (entity?.Count() > 0)
            {
                await _ctx.AddRangeAsync(entity);

            }
            return 0;
        }

        public async Task<int> Update(T entity)
        {
            _ctx.Update(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> Update(IList<T> entity)
        {
            if (entity?.Count > 0)
            {
                _ctx.UpdateRange(entity);
                return await _ctx.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(IEnumerable<T> entity)
        {
            if (entity?.Count() > 0)
            {
                _ctx.UpdateRange(entity);
                return await _ctx.SaveChangesAsync();
            }
            return 0;
        }

        public IQueryable<T> WhereActive(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        {
            T nullEntity;
            var activeExpression = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(whereExpression.Parameters[0], nameof(nullEntity.DeletedAt)),
                    Expression.Constant(null, typeof(DateTime?))
                ),
                whereExpression.Parameters
            );

            var whereBody = whereExpression.Body;
            var activeBody = activeExpression.Body;

            var combinedBody = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(whereBody, activeBody),
                whereExpression.Parameters
            );

            return Where(combinedBody, orderByExpression, ascending);
        }

        public IQueryable<T> WhereSoftDeleted(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        {
            T nullEntity;

            var nonActiveExpression = Expression.Lambda<Func<T, bool>>(
                Expression.NotEqual(
                    Expression.Property(whereExpression.Parameters[0], nameof(nullEntity.DeletedAt)),
                    Expression.Constant(null, typeof(DateTime?))
                ),
                whereExpression.Parameters
            );

            var whereBody = whereExpression.Body;
            var activeBody = nonActiveExpression.Body;

            var combinedBody = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(whereBody, activeBody),
                whereExpression.Parameters
            );

            return Where(combinedBody, orderByExpression, ascending).IgnoreQueryFilters();
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

        public (int,IQueryable<T>) WhereAsPaginateQuerie(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> ordenarPor = null, bool ascendente = true, int page = 1)
        {
            var resultList = new List<T>();
            var querie = WhereActive(whereExpression, ordenarPor, ascendente);
            var count = querie.Count();

            if (page > 1)
            {
                querie = querie.Skip(page * AppSettings.Take - AppSettings.Take).Take(AppSettings.Take);
            }
            else
                querie = querie.Take(AppSettings.Take);
            return (count,querie);
        }

        public async Task<Paginacion<T>> WhereAsPaginateListAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> ordenarPor = null, bool ascendente = true, int page = 1)
        {
            var resultList = new List<T>();
            var querie = WhereActive(whereExpression, ordenarPor, ascendente);
            var count = await querie.CountAsync();
            if (page > 1)
                resultList.AddRange(await querie.Skip(page * AppSettings.Take).Take(AppSettings.Take).ToListAsync());
            else
                resultList.AddRange(await querie.Take(AppSettings.Take).ToListAsync());
            return Paginacion<T>.Crear(resultList, count,page);
        }

        public async Task<bool> Delete(dynamic id)
        {
            if(id is long)
            {
                long longId = id;
                var entity = await WhereActive(x => x.Id == longId).FirstOrDefaultAsync();
                if (entity != null)
                {
                    _ctx.Remove(entity);
                    return true;
                }
                return false;
            }
            throw new NotImplementedException("INVALID_TYPEOF_ENTITY_DELETE");
        }
    }
}
