using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.Services
{
    public class ProductoServices : IProductoServices
    {
        public async Task<ResultResponse> Crear(CrearProductoDTO dTO)
        {
            var result = new ResultResponse();
            return result;
        }

        public async Task<ResultResponse> Editar(EditarProductoDTO dTO)
        {
            var result = new ResultResponse();
            return result;
        }

        public async Task<ResultResponse> Eliminar(string productId)
        {
            var result = new ResultResponse();
            return result;
        }

        public async Task<ResultResponse> Listar(Expression<Func<Producto, bool>> whereExpression)
        {
            var result = new ResultResponse();
            return result;
        }

        public async Task<ResultResponse> Restaurar(string productId)
        {
            var result = new ResultResponse();
            return result;
        }
    }
}
