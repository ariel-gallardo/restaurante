using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IProductoIngredienteServices
    {
        public Task<ResultResponse> Asociar(ProductoIngredienteDTO dTO);
        public Task<ResultResponse> Restaurar(string productoIngredienteId);
    }
}
