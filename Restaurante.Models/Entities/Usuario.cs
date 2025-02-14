using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("usuarios")]
    public class Usuario : BigIntEntity
    {
        [Column("imagen_url")]
        public string ImagenUrl { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Rol Rol { get; set; }
        [ForeignKey("Rol")]
        [Column("rol_id")]
        public long? RolId { get; set; }
        [Column("persona_id")]
        [ForeignKey("Persona")]
        public long? PersonaId { get; set; }
        [NotMapped]
        public virtual Pedido PedidoActual { get; set; }
    }

}
