using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    public class EditarIngredienteDTO
    {
        public IFormFile? Imagen { get; set; }
        [MaxLength(50, ErrorMessage = "ERROR_MAX_LENGTH_50")]
        public string? Nombre { get; set; }
        [MaxLength(200, ErrorMessage = "ERROR_MAX_LENGTH_200")]
        public string? Descripcion { get; set; }
        public double? PrecioDeCompra { get; set; }
        public double? PrecioDeVenta { get; set; }
        public double? StockActual { get; set; }
        public double? StockAlerta { get; set; }
        [MaxLength(2, ErrorMessage = "ERROR_MAX_LENGTH_2")]
        public string? Unidad { get; set; }
        [Required(ErrorMessage = "ID_PRODUCT_REQUIRED")]
        public string IngredienteId { get; set; }
    }
}
