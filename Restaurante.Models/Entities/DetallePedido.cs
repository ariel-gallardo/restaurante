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
    }
}
