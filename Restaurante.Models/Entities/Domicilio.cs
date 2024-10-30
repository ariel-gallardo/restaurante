using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("domicilios")]
    public class Domicilio : BigIntEntity
    {
        [Required]
        [Column("calle")]
        public string Calle { get; set; }
        [Required]
        [Column("numero")]
        public UInt16 Numero { get; set; }
        [Required]
        [Column("localidad")]
        public string Localidad { get; set; }
    }

}
