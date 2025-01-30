using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Examples;
using Restaurante.Const;
using Restaurante.Models;
using Restaurante.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServices _services;
        public CategoriaController(ICategoriaServices services)
        {
            _services = services;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar categoria", Description = "Lista categoria con paginacion.")]
        public async Task<IActionResult> ListarCategorias([FromQuery] int paginaNum = 1, [FromQuery] bool ascendente = true, [FromQuery] string? nombreClave = "", [FromQuery] string? catPadreId = "")
        {
            var operation = await _services.Listar(paginaNum, ascendente,nombreClave, catPadreId); 
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear categoria", Description = "Crear una categoria.")]
        [SwaggerRequestExample(typeof(CrearCategoriaDTO), typeof(CrearCategoriaRequestExample))]
        [CustomAuthorize($"{Roles.Administrador}")]
        public async Task<IActionResult> CrearCategoria(CrearCategoriaDTO dTO)
        {
            var operation = await _services.Crear(dTO);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Editar categoria", Description = "Editar detalles de categoria.")]
        [CustomAuthorize($"{Roles.Administrador}")]
        public async Task<IActionResult> EditarCategoria(EditarCategoriaDTO dTO)
        {
            var operation = await _services.Editar(dTO);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Eliminar Categoria", Description = "Baja logica de categoria.")]
        [CustomAuthorize($"{Roles.Administrador}")]
        public async Task<IActionResult> EliminarCategoria([FromQuery] long categoriaId = 0)
        {
            var operation = await _services.Eliminar(categoriaId);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPatch]
        [SwaggerOperation(Summary = "Restaurar Categoria", Description = "Restaurar categoria baja logica.")]
        [CustomAuthorize($"{Roles.Administrador}")]
        public async Task<IActionResult> RestaurarCategoria([FromQuery] long categoriaId = 0)
        {
            var operation = await _services.Restaurar(categoriaId);
            return StatusCode(operation.StatusCode, operation);
        }
    }
}
