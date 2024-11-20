using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    internal class ProductoIngredienteDTO
    {
        [Required(ErrorMessage = "ID_PRODUCT_REQUIRED")]
        public string ProductoId { get; set; }
        [Required(ErrorMessage = "ID_INGREDIENT_REQUIRED")]
        public string IngredienteId { get; set; }
        public string AnteriorId { get; set; }
        [Required]
        [MaxLength(2, ErrorMessage = "ERROR_MAX_LENGTH_2")]
        public string Unidad { get; set; }
        [Required(ErrorMessage = "QUANTITY_REQUIRED")]
        public double Cantidad { get; set; }
    }
}
