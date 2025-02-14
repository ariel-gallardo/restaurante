using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("detalle_pedido")]
    public class DetallePedido : StringEntity
    {
        
        public virtual Producto Producto { get; set; }
        [Column("cantidad")]
        public double Cantidad { get; set; }
        [Column("subtotal")]
        public double SubTotal { get; set; }
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public string ProductoId { get; set; }
        [Column("pedido_id")]
        public string PedidoId { get; set; }
    }
}
