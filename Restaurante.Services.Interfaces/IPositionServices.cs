using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IPositionServices
    {
        Task<Posicion> PosicionActual(string pedidoId);
        Task<IList<Posicion>> Posiciones(string pedidoId);
        Task<Posicion> Almacenar(PosicionDTO p);
    }
}
