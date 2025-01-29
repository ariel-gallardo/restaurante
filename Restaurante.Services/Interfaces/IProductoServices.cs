using Restaurante.Models;
namespace Restaurante.Services
{
    public interface IProductoServices
    {
        public Task<ResultResponse> Crear(CrearProductoDTO dTO);
        public Task<ResultResponse> Editar(EditarProductoDTO dTO);
        public Task<ResultResponse> Eliminar(string productId);
        public Task<ResultResponse> Restaurar(string productId);
        public Task<ResultResponse> Listar(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0, long? categoria = 0);
        public Task<ResultResponse> Consumir(ConsumirProductoDTO dto);
    }
}
