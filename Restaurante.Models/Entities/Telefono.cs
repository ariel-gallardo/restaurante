using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("telefonos")]
    public class Telefono : BigIntEntity
    {
        [Required]
        [Column("codigo_area")]
        public UInt16 CodigoArea { get; set; }
        [Required]
        [Column("numero")]
        public ulong Numero { get; set; }
    }

}
