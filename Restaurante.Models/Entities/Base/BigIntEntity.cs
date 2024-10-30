using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    public class BigIntEntity
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
