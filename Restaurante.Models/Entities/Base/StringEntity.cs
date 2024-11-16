using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    public class StringEntity
    {
        [Column("id")]
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public override bool Equals(object obj)
        => obj != null && GetHashCode() == obj.GetHashCode() && obj is StringEntity;

        public override int GetHashCode()
        => Id.GetHashCode() + GetType().GetHashCode();
    }
}
