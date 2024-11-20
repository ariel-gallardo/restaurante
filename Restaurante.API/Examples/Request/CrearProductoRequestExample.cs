using Restaurante.Infraestructure;
using Restaurante.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API
{
    public class CrearProductoRequestExample : IMultipleExamplesProvider<CrearProductoDTO>
    {
        public IEnumerable<SwaggerExample<CrearProductoDTO>> GetExamples()
        {
            yield return SwaggerExample.Create("Pizza Muzarella - Chica", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Muzarella - Chica",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaMuzarella)
                }
            );
            yield return SwaggerExample.Create("Pizza Especial - Chica", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Especial - Chica",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaEspecial),
                    Descripcion = "Pizza con jamon y queso."
                }
            );
            yield return SwaggerExample.Create("Pizza Napolitana - Chica", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Napolitana - Chica",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaNapolitana),
                    Descripcion = "Pizza con tomate y albahaca."
                }
            );
            yield return SwaggerExample.Create("Pizza Muzarella - Grande", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Muzarella - Chica",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaMuzarella)
                }
            );
            yield return SwaggerExample.Create("Pizza Especial - Grande", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Especial - Grande",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaEspecial),
                    Descripcion = "Pizza con jamon y queso."
                }
            );
            yield return SwaggerExample.Create("Pizza Napolitana - Grande", 
                new CrearProductoDTO 
                { 
                    Nombre = "Pizza Napolitana - Grande",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PizzaNapolitana),
                    Descripcion = "Pizza con tomate y albahaca."
                }
            );
            yield return SwaggerExample.Create("Pancho simple", 
                new CrearProductoDTO 
                {
                    Nombre = "Pancho simple",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PanchoSimple),
                    Descripcion = "Pancho con lluvia de papas."
                }
            );
            yield return SwaggerExample.Create("Pancho con poncho", 
                new CrearProductoDTO 
                {
                    Nombre = "Pancho con poncho",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.PanchoConPoncho),
                    Descripcion = "Pancho con queso, lluvia de papas y condimentos."
                }
            );
            yield return SwaggerExample.Create("Hamburguesa Simple", 
                new CrearProductoDTO 
                { 
                    Nombre = "Hamburguesa Simple",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.HamburguesaSimple),
                    Descripcion = "Hamburguesa con tomate, lechuga y queso."
                }
            );
            yield return SwaggerExample.Create("Hamburguesa Completa", 
                new CrearProductoDTO 
                {
                    Nombre = "Hamburguesa Completa",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.HamburguesaCompleta),
                    Descripcion = "Hamburguesa con tomate, lechuga, queso, huevo y jamon."
                }
            );
            yield return SwaggerExample.Create("Coca-Cola 2.25L", 
                new CrearProductoDTO 
                { 
                    Nombre = "Coca-Cola 2.25L",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.CocaColaGrande),
                    PrecioDeCompra = 1500,
                    PrecioDeVenta = 3000,
                    StockActual = 30,
                    StockAlerta = 10
                }
            );
            yield return SwaggerExample.Create("Coca-Cola 500ML", 
                new CrearProductoDTO 
                {
                    Nombre = "Coca-Cola 500ML",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.CocaColaChica),
                    PrecioDeCompra = 500,
                    PrecioDeVenta = 1000,
                    StockActual = 50,
                    StockAlerta = 20
                }
            );
            yield return SwaggerExample.Create("Sprite 2.25L", 
                new CrearProductoDTO 
                { 
                    Nombre = "Sprite 2.25L",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.SpriteGrande),
                    PrecioDeCompra = 1200,
                    PrecioDeVenta = 2800,
                    StockActual = 30,
                    StockAlerta = 10
                }
            );
            yield return SwaggerExample.Create("Sprite 500ML", 
                new CrearProductoDTO 
                {
                    Nombre = "Sprite 500ML",
                    Unidad = "U",
                    Imagen = FileStorageService.CreateFormFileFromFile(Restaurante.Assets.Content.Imagenes.SpriteChica),
                    PrecioDeCompra = 400,
                    PrecioDeVenta = 800,
                    StockActual = 50,
                    StockAlerta = 20
                }
            );
        }
    }
}
