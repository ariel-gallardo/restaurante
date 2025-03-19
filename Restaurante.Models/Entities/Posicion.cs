using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("posiciones")]
    public class Posicion : StringEntity
    {
        [ForeignKey("Delivery")]
        [Column("delivery_id")]
        public long DeliveryId { get; set; }
        [ForeignKey("Pedido")]
        [Column("pedido_id")]
        public string PedidoId { get; set; }
        [Column("latitud")]
        public double Latitud { get; set; }
        [Column("longitud")]
        public double Longitud { get; set; }
        [Column("velocidad")]
        public ushort Velocidad { get; set; }
        [Column("direccion")]
        public ushort Direccion { get; set; }
        [Column("tiempo")]
        public DateTimeOffset Tiempo { get; set; }
        public virtual Usuario Delivery { get; set; }
        public virtual Pedido Pedido { get; set; }

    }
}
