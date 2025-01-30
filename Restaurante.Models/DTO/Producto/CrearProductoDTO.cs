using Microsoft.AspNetCore.Http;
using Restaurante.Infraestructure;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Models
{
    [SwaggerSchema("Crear un producto")]
    public class CrearProductoDTO
    {
        /// <summary>
        /// Id
        /// Cantidad
        /// Unidad
        /// </summary>
        public (string, double, string)[]? Ingredientes { get; set; }
        public CustomFormFile? Imagen { get; set; }
        [MaxLength(50, ErrorMessage = "ERROR_MAX_LENGTH_50")]
        [Required(ErrorMessage = "NAME_REQUIRED")]
        [Trim]
        public string Nombre { get; set; }
        [MaxLength(200, ErrorMessage = "ERROR_MAX_LENGTH_200")]
        [Trim]
        public string? Descripcion { get; set; }
        public double? PrecioDeCompra { get; set; }
        public double? PrecioDeVenta { get; set; }
        public double? StockActual { get; set; }
        public double? StockAlerta { get; set; }
        [Required]
        [MaxLength(2, ErrorMessage = "ERROR_MAX_LENGTH_2")]
        public string Unidad { get; set; }
        public long? CategoriaId { get; set; }
    }
}
