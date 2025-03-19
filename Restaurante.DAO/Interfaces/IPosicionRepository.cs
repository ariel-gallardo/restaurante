using Restaurante.Models;

namespace Restaurante.DAO
{
    public interface IPosicionRepository : IRepository<Posicion>
    {
        Task<Posicion> Almacenar(Posicion p);
        Task<IList<Posicion>> AlmacenarPosiciones(Posicion p);
        Task<Posicion> PosicionActual(string pedidoId);
        Task<IList<Posicion>> Posiciones(string pedidoId);
    }
}
