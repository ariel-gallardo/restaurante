using Restaurante.Infraestructure;
using Restaurante.Models;

namespace Restaurante.Services
{
    public class PaginacionService : IPaginacionService
    {
        public Paginacion<T> Ejecutar<T>(IEnumerable<T> data, int total)
        => new Paginacion<T>(data, total, AppSettings.Take);

        public Paginacion<T> Ejecutar<T>(IList<T> data, int total)
        => new Paginacion<T>(data, total, AppSettings.Take);
    }
}
