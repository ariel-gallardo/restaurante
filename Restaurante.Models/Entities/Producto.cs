
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("productos")]
    public class Producto : StringEntity
    {
        public virtual IList<ProductoIngrediente> Ingredientes { get; set; }
        [Column("imagen")]
        public string ImagenUrl { get; set; }
        [Column("precio_compra")]
        public double PrecioCompra { get; set; }
        [Column("precio_venta")]
        public double PrecioVenta { get; set; }
        [Column("stock_actual")]
        public double StockActual { get; set; }
        [Column("stock_alerta")]
        public double StockAlerta { get; set; }
        [Column("unidad")]
        public string Unidad { get; set; }
    }
}
