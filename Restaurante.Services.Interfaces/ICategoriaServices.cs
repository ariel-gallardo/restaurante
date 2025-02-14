
using Restaurante.Models;

namespace Restaurante.Services
{
    public interface ICategoriaServices
    {
        public Task<ResultResponse> Crear(CrearCategoriaDTO dTO);
        public Task<ResultResponse> Editar(EditarCategoriaDTO dTO);
        public Task<ResultResponse> Eliminar(long categoriaId);
        public Task<ResultResponse> Listar(int paginaNum = 1, bool ascendente = true, string nombreClave = "", string catPadreId = "");
        public Task<ResultResponse> Restaurar(long categoriaId);
    }
}
