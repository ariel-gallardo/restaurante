using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class CrearCategoriaDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string CategoriaPadre { get; set; } = "";
        [Required(AllowEmptyStrings = true)]
        public string CategoriaPadreId { get; set; } = "";
        [Required(AllowEmptyStrings = true)]
        public string Descripcion { get; set; } = "";
    }
}
