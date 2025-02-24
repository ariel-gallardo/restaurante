using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("producto_ingrediente")]
    public class ProductoIngrediente : StringEntity
    {
        [ForeignKey("IngredienteId")]
        public virtual Ingrediente Ingrediente { get; set; }
        
        public virtual Producto Producto { get; set; }
        [Column("ingrediente_id")]
        public string IngredienteId { get; set; }
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public string ProductoId { get; set; }
        [Column("unidad")]
        public string Unidad { get; set; }
        [Column("cantidad")]
        public double Cantidad { get; set; }
    }
}
