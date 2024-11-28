using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;

namespace Restaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IProductoServices _productoServices;
        private readonly IPedidoServices _pedidoServices;

        public ClienteController(IProductoServices productoServices, IPedidoServices pedidoServices)
        {
            _productoServices = productoServices;
            _pedidoServices = pedidoServices;
        }

        
        [HttpPost("consumir-producto")]
        [CustomAuthorize]
        public async Task<IActionResult> ConsumirProducto([FromBody] ConsumirProductoDTO dto)
        {
            var operation = await _productoServices.Consumir(dto);
            return StatusCode(operation.StatusCode,operation);
        }

        [HttpPost("interacturar-pedido")]
        [CustomAuthorize]
        public async Task<IActionResult> InteractuarPedidoActual([FromBody] PedidoDTO dTO)
        {
            var operation = await _pedidoServices.InteractuarPedidoActual(dTO);
            return StatusCode(operation.StatusCode, operation);
        }
    }
}
