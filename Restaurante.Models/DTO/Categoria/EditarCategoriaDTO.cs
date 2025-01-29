using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class EditarCategoriaDTO
    {
        [Required(ErrorMessage = "ID_CATEGORY_REQUIRED")]
        public long? CategoriaId { get; set; }
        public long? CategoriaPadreId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
