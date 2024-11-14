using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _services;

        public ProductoController(IProductoServices services)
        {
            _services = services;
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(CrearProductoDTO), typeof(CrearProductoRequestExample))]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoDTO dto)
        {
            var operation = await _services.Crear(dto);
            if (operation.StatusCode != StatusCodes.Status201Created)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            return StatusCode(200);
        }
    }
}
