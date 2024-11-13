using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.Services
{
    public interface IProductoServices
    {
        public Task<ResultResponse> Crear(CrearProductoDTO dTO);
        public Task<ResultResponse> Editar(EditarProductoDTO dTO);
        public Task<ResultResponse> Eliminar(string productId);
        public Task<ResultResponse> Restaurar(string productId);
        public Task<ResultResponse> Listar(Expression<Func<Producto,bool>> whereExpression, int page);
    }
}
