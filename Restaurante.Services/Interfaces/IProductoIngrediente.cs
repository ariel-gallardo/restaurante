using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IProductoIngrediente
    {
        public Task<ResultResponse> Crear(ProductoIngredienteDTO dTO);
        public Task<ResultResponse> Editar(ProductoIngredienteDTO dTO);
        public Task<ResultResponse> Eliminar(string productoIngredienteId);
        public Task<ResultResponse> Restaurar(string productoIngredienteId);
    }
}
