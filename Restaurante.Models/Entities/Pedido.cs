using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("pedidos")]
    public class Pedido : StringEntity
    {
        [Column("pedido_id")]
        public virtual IList<DetallePedido> Detalles { get; set; }
        [Column("estado")]
        public string Estado { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [Column("usuario_id")]
        public long UsuarioId { get; set; }
    }
}
