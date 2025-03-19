using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Restaurante.DAO;
using Restaurante.Hubs;
using Restaurante.Models;

namespace Restaurante.Services
{
    public class PositionServices : IPositionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<PedidoHub> _pHub;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public PositionServices(IUnitOfWork unitOfWork, IHubContext<PedidoHub> pHub, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _pHub = pHub;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Posicion> PosicionActual(string pedidoId)
        {
            var p = await _unitOfWork.Posicion.PosicionActual(pedidoId);
            return p;
        }

        public async Task<IList<Posicion>> Posiciones(string pedidoId)
        {
            var p = await _unitOfWork.Posicion.Posiciones(pedidoId);
            return p;
        }

        public async Task<Posicion> Almacenar(PosicionDTO p)
        {
            var pos = _mapper.Map<PosicionDTO, Posicion>(p);
            var deliveryId = await _unitOfWork.Pedido.DeliveryId(pos.PedidoId);
            if (deliveryId == null) throw new Exception($@"DELIVERY_NOT_ASSIGNED ""{pos.PedidoId}""");
            pos.DeliveryId = deliveryId.Value;
            pos.PedidoId = pos.PedidoId;
            await Task.WhenAll(
             _unitOfWork.Posicion.Almacenar(pos),
             _pHub.Clients.Group($"{pos.PedidoId}").SendAsync("Posicion", _mapper.Map<PosicionDTO>(pos))
            );
            return pos;
        }

    }
}
