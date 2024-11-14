using Restaurante.Infraestructure;
using Restaurante.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API
{
    public class CrearProductoRequestExample : IMultipleExamplesProvider<CrearProductoDTO>
    {
        public IEnumerable<SwaggerExample<CrearProductoDTO>> GetExamples()
        {
            yield return SwaggerExample.Create("Pizza Muzarella", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Muzarella",
                    Unidad = "Unidad",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaMuzarella)
                }
            );
            yield return SwaggerExample.Create("Pizza Especial", 
                new CrearProductoDTO 
                { 
                    Nombre = "",
                    Unidad = "Unidad",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaEspecial)
                }
            );
            yield return SwaggerExample.Create("Pizza Napolitana", 
                new CrearProductoDTO 
                { 
                    Nombre = "",
                    Unidad = "Unidad",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaNapolitana)
                }
            );
            yield return SwaggerExample.Create("Pancho", new CrearProductoDTO { });
            yield return SwaggerExample.Create("Hamburguesa", new CrearProductoDTO { });
            yield return SwaggerExample.Create("Coca-Cola 2.25L", new CrearProductoDTO { });
            yield return SwaggerExample.Create("Coca-Cola 500ML", new CrearProductoDTO { });
        }
    }
}
