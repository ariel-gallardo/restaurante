using Microsoft.AspNetCore.Mvc;
using Restaurante.Const;
using Restaurante.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoServices _services;

        public PedidoController(IPedidoServices services)
        {
            _services = services;
        }

        [HttpGet]
        [CustomAuthorize($"{Roles.Delivery},{Roles.Cocinero},{Roles.Recepcionista}")]
        [SwaggerOperation(Summary = "Obtener Pedido", Description = "Indispensable para el rol de personas del trabajo o simplemente buscar uno.")]
        public async Task<IActionResult> Pedidos([FromQuery] string? id, [FromQuery] bool ascendente = true, [FromQuery] int paginaNum = 1)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var operation = await _services.BuscarPorId(id);
                return StatusCode(operation.StatusCode, operation);
            }else
            {
                var operation = await _services.BuscarActualesPorRol(ascendente, paginaNum);
                return StatusCode(operation.StatusCode, operation);
            }
        }
    }
}
