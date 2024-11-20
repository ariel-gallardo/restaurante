using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Const;
using Restaurante.Models;
using Restaurante.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _services;
        private readonly IProductoIngredienteServices _prodIngServices;

        public ProductoController(IProductoServices services, IProductoIngredienteServices prodIngServices)
        {
            _services = services;
            _prodIngServices = prodIngServices;
        }

        [HttpPost]
        [CustomAuthorize($"{Roles.Administrador},{Roles.Cocinero}")]
        [SwaggerRequestExample(typeof(CrearProductoDTO), typeof(CrearProductoRequestExample))]
        [SwaggerOperation(Summary = "Crear un producto", Description = "Crear un producto - Administrador | Cocinero.")]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoDTO dto)
        {
            var operation = await _services.Crear(dto);
            if (operation.StatusCode != StatusCodes.Status201Created)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPut]
        [CustomAuthorize($"{Roles.Administrador},{Roles.Cocinero}")]
        [SwaggerOperation(Summary = "Editar un producto", Description = "Editar un producto - Administrador | Cocinero.")]
        public async Task<IActionResult> EditarProducto([FromBody] EditarProductoDTO dto)
        {
            var operation = await _services.Editar(dto);
            if (operation.StatusCode != StatusCodes.Status200OK)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpDelete]
        [CustomAuthorize($"{Roles.Administrador},{Roles.Cocinero}")]
        [SwaggerOperation(Summary = "Eliminar un producto", Description = "Eliminar un producto - Administrador | Cocinero.")]
        public async Task<IActionResult> EliminarProducto([FromQuery] string productId)
        {
            var operation = await _services.Eliminar(productId);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPatch]
        [CustomAuthorize($"{Roles.Administrador}")]
        [SwaggerOperation(Summary = "Restaurar un producto", Description = "Restaurar un producto - Administrador.")]
        public async Task<IActionResult> RestaurarProducto([FromQuery] string productId)
        {
            var operation = await _services.Restaurar(productId);
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar productos", Description = "Listar productos - Todos.")]
        public async Task<IActionResult> ListarProductos([FromQuery] int? paginaNum = 1, [FromQuery] string? ordenarPor = "", [FromQuery] bool? ascendente = true, [FromQuery] string? nombreClave = "", [FromQuery] double? precioMin = 0.0, [FromQuery] double? precioMax = 0.0)
        {
            var operation = await _services.Listar(paginaNum,ordenarPor,ascendente,nombreClave,precioMin,precioMax);
            return StatusCode(operation.StatusCode,operation);
        }

        [HttpPut("asociar-ingrediente")]
        [CustomAuthorize($"{Roles.Cocinero},{Roles.Administrador}")]
        public async Task<IActionResult> AsociarIngrediente([FromBody] ProductoIngredienteDTO dto)
        {
            var operation = await _prodIngServices.Asociar(dto);
            return StatusCode(operation.StatusCode, operation);
        }
    }
}
