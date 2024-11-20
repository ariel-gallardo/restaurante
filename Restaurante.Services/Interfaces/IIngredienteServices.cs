using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IIngredienteServices
    {
        public Task<ResultResponse> Crear(CrearIngredienteDTO dTO);
        public Task<ResultResponse> Editar(EditarIngredienteDTO dTO);
        public Task<ResultResponse> Eliminar(string ingredienteId);
        public Task<ResultResponse> Restaurar(string ingredienteId);
        public Task<ResultResponse> Listar(int? paginaNum = 1, [FromQuery] string? productoId = "", string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0, bool? porPrecioVenta = true);
    }
}
