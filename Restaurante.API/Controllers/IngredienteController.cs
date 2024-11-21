using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Examples;
using Restaurante.Const;
using Restaurante.Models;
using Restaurante.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteServices _services;

        public IngredienteController(IIngredienteServices services)
        {
            _services = services;            
        }

        [HttpPost]
        [CustomAuthorize($"{Roles.Administrador},{Roles.Cocinero}")]
        [SwaggerRequestExample(typeof(CrearIngredienteDTO), typeof(CrearIngredienteRequestExample))]
        [SwaggerOperation(Summary = "Crear un ingrediente", Description = "Crear un ingrediente - Administrador | Cocinero.")]
        public async Task<IActionResult> CrearIngrediente([FromBody] CrearIngredienteDTO dto)
        {
            var operation = await _services.Crear(dto);
            if (operation.StatusCode != StatusCodes.Status201Created)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPut]
        [CustomAuthorize($"{Roles.Administrador},{Roles.Cocinero}")]
        [SwaggerOperation(Summary = "Editar un ingrediente", Description = "Editar un ingrediente - Administrador | Cocinero.")]
        public async Task<IActionResult> EditarIngrediente([FromBody] EditarIngredienteDTO dto)
        {
            var operation = await _services.Editar(dto);
            if (operation.StatusCode != StatusCodes.Status200OK)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpDelete]
        [CustomAuthorize($"{Roles.Administrador},{Roles.Cocinero}")]
        [SwaggerOperation(Summary = "Eliminar un ingrediente", Description = "Eliminar un ingrediente - Administrador | Cocinero.")]
        public async Task<IActionResult> EliminarIngrediente([FromQuery] string ingredientId)
        {
            var operation = await _services.Eliminar(ingredientId);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPatch]
        [CustomAuthorize($"{Roles.Administrador}")]
        [SwaggerOperation(Summary = "Restaurar un ingrediente", Description = "Restaurar un ingrediente - Administrador.")]
        public async Task<IActionResult> RestaurarIngrediente([FromQuery] string ingredientId)
        {
            var operation = await _services.Restaurar(ingredientId);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar ingredientes", Description = "Listar ingredientes - Todos.")]
        public async Task<IActionResult> ListarIngredientes([FromQuery] int? paginaNum = 1, [FromQuery] string? productoId = "", [FromQuery] string? ordenarPor = "", [FromQuery] bool? ascendente = true, [FromQuery] string? nombreClave = "", [FromQuery] double? precioMin = 0.0, [FromQuery] double? precioMax = 0.0, [FromQuery] bool? porPrecioVenta = true)
        {
            var operation = await _services.Listar(paginaNum, productoId, ordenarPor, ascendente, nombreClave, precioMin, precioMax, porPrecioVenta);
            return StatusCode(operation.StatusCode, operation);
        }
    }
}
