using Microsoft.EntityFrameworkCore;
using Restaurante.Infraestructure;
using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.DAO
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(IRepository<Producto> repository) : base(repository)
        {
        }

        public async Task<bool> ExistsProducto(string nombreProducto)
        => await Where(x => EF.Functions.Like(x.Nombre, nombreProducto) && x.DeletedAt == null).CountAsync() < 1;

        public async Task<Producto> CrearProducto(Producto entity)
        {
            if (await ExistsProducto(entity.Nombre))
            {
                await Insert(entity);
                await UnitOfWork.ProductoIngrediente.Insert(entity.Ingredientes);
                await UnitOfWork.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<bool> EditarProducto(Producto entity, Producto newProperties)
        {
            if (entity != null)
            {
                Update(newProperties);
                UnitOfWork.ProductoIngrediente.Update(newProperties.Ingredientes.Where(x => entity.Ingredientes.Contains(x)));
                UnitOfWork.ProductoIngrediente.Delete(entity.Ingredientes.Where(x => !newProperties.Ingredientes.Contains(x) && x.CreatedAt != null));
                await UnitOfWork.ProductoIngrediente.Insert(newProperties.Ingredientes.Where(x => !entity.Ingredientes.Contains(x) && x.CreatedAt == null));
                await UnitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<(IList<Producto>,int)> ListarProductos(Expression<Func<Producto, bool>> whereExpression, int page)
        {
            var resultList = new List<Producto>();
            var querie = WhereActive(whereExpression, x => x.Nombre, true);
            var count = await querie.CountAsync();
            if (page > 1)
                resultList.AddRange(await querie.Skip(page * AppSettings.Take).Take(AppSettings.Take).ToListAsync());
            else
                resultList.AddRange(await querie.Take(AppSettings.Take).ToListAsync());
            return (resultList,count);
        }
    }
}
