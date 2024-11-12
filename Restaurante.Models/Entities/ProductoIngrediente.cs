using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("producto_ingrediente")]
    public class ProductoIngrediente : StringEntity
    {
        public virtual Ingrediente Ingrediente { get; set; }
        public virtual Producto Producto { get; set; }
        [Column("ingrediente_id")]
        public string IngredienteId { get; set; }
        [Column("producto_id")]
        public string ProductoId { get; set; }
        [Column("unidad")]
        public string Unidad { get; set; }
        [Column("cantidad")]
        public double Cantidad { get; set; }
    }
}
