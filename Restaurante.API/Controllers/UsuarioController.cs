using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUserServices _services;
        public UsuarioController(IUserServices services) 
        {
            _services = services;
        }

        [HttpPost("register")]
        [SwaggerRequestExample(typeof(RegisterDTO), typeof(RegisterRequestExample))]
        [SwaggerOperation(Summary = "Registrarse como cliente", Description = "Registrarse como cliente | Todos.")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO dto)
        {
            var operation = await _services.Register(dto);
            if (operation.StatusCode != StatusCodes.Status201Created)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPost("login")]
        [SwaggerRequestExample(typeof(LoginDTO),typeof(LoginRequestExample))]
        [SwaggerOperation(Summary = "Iniciar Sesion", Description = "Iniciar Sesion | Usuarios no autenticados.")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO dto)
        {
            var operation = await _services.Login(dto);
            if (operation.StatusCode == StatusCodes.Status200OK)
            {
                Response.Headers.Add("Authorization", $"Bearer {operation.Content.Item2}");
                return Ok(operation.Content.Item1);
            }
            else
            {
                operation.Content = dto;
                return StatusCode(operation.StatusCode, operation);
            }
        }

        [HttpGet("info")]
        [CustomAuthorize]
        [SwaggerOperation(Summary = "Informacion de Usuario", Description = "Visualizar informacion del usuario | Usuarios autenticados.")]
        public async Task<IActionResult> InfoUser()
        {
            var authorization = Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "authorization").Value;
            return Ok(await _services.Info(authorization));
        }
    }
}
