using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurante.Models
{
    [Table("categorias")]
    public class Categoria : BigIntEntity
    {
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
        public virtual Categoria CategoriaPadre { get; set; }
        [Column("categoria_id")]
        public long? CategoriaPadreId {  get; set; }
    }
}
