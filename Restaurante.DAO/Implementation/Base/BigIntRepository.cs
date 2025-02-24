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
        private RestauranteContext Context => UnitOfWork.Context;

        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<int> Delete(T entity)
        {
            Context.Remove(entity);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Delete(IList<T> entity)
        {
            if (entity?.Count > 0)
            {
                Context.RemoveRange(entity);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(IEnumerable<T> entity)
        {
            if (entity?.Count() > 0)
            {
                Context.RemoveRange(entity);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null, bool ascending = false)
        {
            var expression = Context.Set<T>().Where(whereExpression);

            if(orderByExpression != null)
            expression = ascending ? expression.OrderBy(orderByExpression) : expression.OrderByDescending(orderByExpression);

            return expression;
        }

        public async Task<int> Insert(T entity)
        {
            await Context.AddAsync(entity);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Insert(IList<T> entity)
        {
            if (entity?.Count > 0)
            {
                await Context.AddRangeAsync(entity);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Insert(IEnumerable<T> entity)
        {
            if (entity?.Count() > 0)
            {
                await Context.AddRangeAsync(entity);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(T entity)
        {
            Context.Update(entity);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> Update(IList<T> entity)
        {
            if (entity?.Count > 0)
            {
                Context.UpdateRange(entity);
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(IEnumerable<T> entity)
        {
            if (entity?.Count() > 0)
            {
                Context.UpdateRange(entity);
                return await Context.SaveChangesAsync();
            }
            return 0;
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
        public bool Exists(dynamic id)
        {
            long cId = 0L;
            if(id != null) long.TryParse(id, out cId);
            return cId > 0 ? Where(x => x.Id == cId).Take(1).Count() == 1 : false;
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
            var querie = Where(whereExpression, ordenarPor, ascendente);
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
            var querie = Where(whereExpression, ordenarPor, ascendente);
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
                var entity = await Where(x => x.Id == longId).FirstOrDefaultAsync();
                if (entity != null)
                {
                    Context.Remove(entity);
                    return true;
                }
                return false;
            }
            throw new NotImplementedException("INVALID_TYPEOF_ENTITY_DELETE");
        }
    }
}
