using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IPaginacionService
    {
        Paginacion<T> Ejecutar<T>(IEnumerable<T> data, int total);
        Paginacion<T> Ejecutar<T>(IList<T> data, int total);
    }
}
