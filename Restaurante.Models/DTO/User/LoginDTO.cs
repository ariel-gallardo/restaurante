using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    [SwaggerSchema("Iniciar sesion")]
    public class LoginDTO
    {
        [Required(ErrorMessage = "EMAIL_REQUIRED")]
        [EmailAddress(ErrorMessage = "EMAIL_REQUIRED")]
        [Trim]
        public string Correo { get; set; }

        [Required(ErrorMessage = "PASSWORD_REQUIRED")]
        [TrimAll]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{8,25}$",
        ErrorMessage = "AMIN_AMAY_ASYM")]
        public string Password { get; set; }
    }
}
