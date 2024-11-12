using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("ingredientes")]
    public class Ingrediente : StringEntity
    {
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
        [Required]
        [MaxLength(2)]
        public string Unidad { get; set; }
        [Column("nombre")]
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }
        [MaxLength(200)]
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
