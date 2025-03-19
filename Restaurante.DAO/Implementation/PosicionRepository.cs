using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class PosicionRepository : BaseRepository<Posicion>, IPosicionRepository
    {
        private readonly IDistributedCache _cache;
        public PosicionRepository(IRepository<Posicion> repository,
            IDistributedCache cache
            ) : base(repository)
        {
            _cache = cache;
        }

        public async Task<Posicion> Almacenar(Posicion p)
        {
            await Insert(p);
            var dataText = JsonSerializer.Serialize(p);
            await Task.WhenAll(
                _cache.SetStringAsync($"Posicion_{p.PedidoId}", dataText),
                AlmacenarPosiciones(p)
            );
            return p;
        }

        public async Task<IList<Posicion>> AlmacenarPosiciones(Posicion p)
        {
            var text = await _cache.GetStringAsync($"Posiciones_{p.PedidoId}");
            var posiciones = new List<Posicion>();
            if (string.IsNullOrEmpty(text))
            {
                posiciones.AddRange(await Where(x => x.PedidoId == p.PedidoId, x => x.Tiempo).ToListAsync());
                posiciones.Insert(0, p);
                await _cache.SetStringAsync($"Posiciones_{p.PedidoId}", JsonSerializer.Serialize(posiciones));
            }
            else
            {
                posiciones.AddRange(JsonSerializer.Deserialize<IList<Posicion>>(text));
                posiciones.Insert(0, p);
                await _cache.SetStringAsync($"Posiciones_{p.PedidoId}", JsonSerializer.Serialize(posiciones));
            }
            return posiciones;
        }

        public async Task<Posicion> PosicionActual(string pedidoId)
        {
            var text = await _cache.GetStringAsync($"Posicion_{pedidoId}");
            Posicion p = null;
            if (string.IsNullOrEmpty(text))
            {
                p = await Where(x => x.PedidoId == pedidoId, x => x.Tiempo).FirstOrDefaultAsync();
                text = JsonSerializer.Serialize(p);
                if (p != null) await Task.WhenAll(
                    _cache.SetStringAsync(
                        $"Posicion_{pedidoId}", text
                    ),
                    AlmacenarPosiciones(p)
                );
            }
            else
            {
                p = JsonSerializer.Deserialize<Posicion>(text);
            }
            return p;
        }



        public async Task<IList<Posicion>> Posiciones(string pedidoId)
        {
            var text = await _cache.GetStringAsync($"Posiciones_{pedidoId}");
            var posiciones = new List<Posicion>();
            if (string.IsNullOrEmpty(text))
            {
                posiciones.AddRange(await Where(x => x.PedidoId == pedidoId, x => x.Tiempo).ToListAsync());
                if (posiciones.Count > 0)
                {
                    text = JsonSerializer.Serialize(posiciones);
                    await _cache.SetStringAsync($"Posiciones_{pedidoId}", text);
                }
            }
            else
            {
                posiciones.AddRange(JsonSerializer.Deserialize<IList<Posicion>>(text));
            }
            return posiciones;
        }
    }
}
