using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    [SwaggerSchema("Obtener informacion de el usuario")]
    public class UserInfoDTO
    {
        public string Correo { get; set; }
        public string TipoDeUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string CaducaEn { get; set; }
    }
}
