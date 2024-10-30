using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("personas")]
    public class Persona : BigIntEntity
    {
        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        public string Apellido { get; set; }
        [Column("telefono_id")]
        public long? TelefonoId { get; set; }
        [Column("domicilio_id")]
        public long? DomicilioId { get; set; }

        public virtual Domicilio Domicilio { get; set; }

        public virtual Telefono Telefono { get; set; }
    }

}
