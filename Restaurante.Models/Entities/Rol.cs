using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("roles")]
    public class Rol : BigIntEntity
    {
        [Required]
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }

}
