using AutoMapper;
using Restaurante.Models;

namespace Restaurante.Services
{
    public class PedidoServices : IPedidoServices
    {
        private readonly IMapper _mapper;

        public PedidoServices(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ResultResponse> InteractuarPedidoActual(PedidoDTO dTO)
        {
            var result = new ResultResponse();
            result.StatusCode = 200;
            result.Message = "On_Development";

            var newStatus = dTO.Estado;
            var newPedidoList = dTO.Data != null ? _mapper.Map<IList<DetallePedido>>(dTO.Data) : null;

            return result;
        }
    }
}
