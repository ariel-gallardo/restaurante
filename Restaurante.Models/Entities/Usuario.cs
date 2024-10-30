using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("usuarios")]
    public class Usuario : BigIntEntity
    {
        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        [ForeignKey("PersonaId")]
        public virtual Persona Persona { get; set; }
        [ForeignKey("RolId")]
        public virtual Rol Rol { get; set; }
        [Column("rol_id")]
        public long? RolId { get; set; }
        [Column("persona_id")]
        public long? PersonaId { get; set; }
    }

}
