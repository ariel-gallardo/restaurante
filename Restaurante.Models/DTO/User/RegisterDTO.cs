using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    [SwaggerSchema("Registrar un usuario")]
    public class RegisterDTO
    {
        [Required(ErrorMessage = "NAME_REQUIRED")]
        [SwaggerSchema(Description = "Nombre de la persona")]
        [Trim]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "SURNAME_REQUIRED")]
        [SwaggerSchema(Description = "Apellido de la persona")]
        [Trim]
        public string Apellido { get; set; }
        [Trim]
        [Required(ErrorMessage = "MAIL_REQUIRED")]
        [EmailAddress(ErrorMessage = "EMAIL_REQUIRED")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "PASSWORD_REQUIRED")]
        [TrimAll]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "BETWEEN_8_25_CHAR")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.*\d).{8,25}$",
        ErrorMessage = "AMIN_AMAY_ASYM")]
        public string Password { get; set; }

        [Required(ErrorMessage = "REPASSWORD_REQUIRED")]
        [TrimAll]
        [ComparePassword("Password", ErrorMessage = "PASSWORD_NOT_EQ")]
        public string RePassword { get; set; }

        [Range(typeof(UInt16), "1", "999")]
        public UInt16? CodigoArea { get; set; }
        [Range(typeof(ulong),"1000000", "9999999")]

        public ulong? NumeroTelefono { get; set; }

        [StringLength(50,ErrorMessage = "MAX_50_CHARS")]
        [Trim]
        public string Calle { get; set; }
        [Range(1,10000)]
        public UInt16? Numero { get; set; }
        [Required]
        public string Localidad { get; set; }
    }
}
