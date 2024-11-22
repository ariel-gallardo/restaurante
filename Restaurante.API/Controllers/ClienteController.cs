using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;

namespace Restaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IProductoServices _pServices;

        public ClienteController(IProductoServices pServices)
        {
            _pServices = pServices;
        }

        
        [HttpPost("consumir-producto")]
        [CustomAuthorize]
        public async Task<IActionResult> ConsumirProducto([FromBody] ConsumirProductoDTO dto)
        {
            var operation = await _pServices.Consumir(dto);
            return StatusCode(operation.StatusCode,operation);
        }
    }
}
