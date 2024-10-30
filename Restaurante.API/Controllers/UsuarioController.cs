using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using Restaurante.Services;
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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO dto)
        {
            var operation = await _services.Register(dto);
            if (operation.StatusCode != StatusCodes.Status201Created)
                operation.Content = dto;
            return StatusCode(operation.StatusCode, operation);
        }

        [HttpPost("login")]
        [SwaggerRequestExample(typeof(LoginDTO),typeof(LoginRequestExample))]
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
        [Authorize]
        public async Task<IActionResult> InfoUser()
        {
            var authorization = Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "authorization").Value;
            return Ok(await _services.Info(authorization));
        }
    }
}
