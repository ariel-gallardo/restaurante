using Restaurante.Infraestructure;

namespace Restaurante.Models
{
    public class Paginacion<T>
    {
        public int CantidadActual { get; set; }
        public int? PaginaActual { get; set; }
        public int? PaginaAnterior { get; set; }
        public int? PaginaSiguiente { get; set; }
        public int Total { get; set; }
        public int PaginasTotales { get; set; }
        public dynamic Content { get; set; }

        public Paginacion(IEnumerable<T> data, int total, int pageSize, int page)
        {
            Total = total;
            CantidadActual = total > 0 ? data.Count() : 0;  
            if(total > 0)
            {
                PaginaActual = page;
                PaginasTotales = (int)Math.Ceiling((double)total / pageSize);
                PaginaAnterior = PaginaActual > 1 ? PaginaActual - 1 : null;
                PaginaSiguiente = PaginaActual < PaginasTotales ? PaginaActual + 1 : null;
                Content = data;
            }
        }

        public Paginacion(IList<T> data, int total, int pageSize, int page)
        {
            Total = total;
            CantidadActual = total > 0 ? data.Count() : 0;
            if (total > 0)
            {
                PaginaActual = page;
                PaginasTotales = (int)Math.Ceiling((double)total / pageSize);
                PaginaAnterior = PaginaActual > 1 ? PaginaActual - 1 : null;
                PaginaSiguiente = PaginaActual < PaginasTotales ? PaginaActual + 1 : null;
                Content = data;
            }
        }

        public static Paginacion<T> Crear<T>(IEnumerable<T> data, int total, int page)
        => new Paginacion<T>(data, total, AppSettings.Take, page);

        public static Paginacion<T> Crear<T>(IList<T> data, int total, int page)
        => new Paginacion<T>(data, total, AppSettings.Take, page);
    }
}
