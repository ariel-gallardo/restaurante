using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class ProductoIngredienteDTO
    {
        public class DataDTO
        {
            public string Id { get; set; }
            [Required(ErrorMessage = "ID_INGREDIENT_REQUIRED")]
            public string IngredienteId { get; set; }
            [Required]
            [MaxLength(2, ErrorMessage = "ERROR_MAX_LENGTH_2")]
            public string Unidad { get; set; }
            [Required(ErrorMessage = "QUANTITY_REQUIRED")]
            public double Cantidad { get; set; }
        }

        [Required(ErrorMessage = "ID_PRODUCT_REQUIRED")]
        public string ProductoId { get; set; }
        public IList<DataDTO> Data { get; set; }
    }

}
