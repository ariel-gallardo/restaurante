using Restaurante.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API.Examples
{
    public class CrearCategoriaRequestExample : IMultipleExamplesProvider<CrearCategoriaDTO>
    {
        public IEnumerable<SwaggerExample<CrearCategoriaDTO>> GetExamples()
        {
            yield return SwaggerExample.Create("Pizzas", new CrearCategoriaDTO
            {
                Nombre = "PIZZAS"
            });
            yield return SwaggerExample.Create("Hamburguesas", new CrearCategoriaDTO
            {
                Nombre = "HAMBURGERS"
            });
            yield return SwaggerExample.Create("Condimentos", new CrearCategoriaDTO
            {
                Nombre = "CONDIMENTS"
            });
            yield return SwaggerExample.Create("Vegetales", new CrearCategoriaDTO
            {
                Nombre = "VEGETABLES"
            });
            yield return SwaggerExample.Create("Carnes", new CrearCategoriaDTO
            {
                Nombre = "MEATS"
            });
            yield return SwaggerExample.Create("Embutidos", new CrearCategoriaDTO
            {
                Nombre = "SAUSAGES"
            });
            yield return SwaggerExample.Create("Quesos", new CrearCategoriaDTO
            {
                Nombre = "CHEESES",
                CategoriaPadre = "DAIRY"
            });
            yield return SwaggerExample.Create("Aceites", new CrearCategoriaDTO
            {
                Nombre = "OILS"
            });
            yield return SwaggerExample.Create("Harinas", new CrearCategoriaDTO
            {
                Nombre = "FLOURS"
            });
            yield return SwaggerExample.Create("Snacks", new CrearCategoriaDTO
            {
                Nombre = "SNACKS"
            });
            yield return SwaggerExample.Create("Gaseosas", new CrearCategoriaDTO
            {
                Nombre = "SODAS",
                CategoriaPadre = "NON_ALCOHOLIC"
            });
            yield return SwaggerExample.Create("Jugos Naturales", new CrearCategoriaDTO
            {
                Nombre = "NATURAL_JUICES",
                CategoriaPadre = "NON_ALCOHOLIC"
            });
            yield return SwaggerExample.Create("Vinos", new CrearCategoriaDTO
            {
                Nombre = "WINES",
                CategoriaPadre = "ALCOHOLIC"
            });
            yield return SwaggerExample.Create("Cervezas", new CrearCategoriaDTO
            {
                Nombre = "BEERS",
                CategoriaPadre = "ALCOHOLIC"
            });
            yield return SwaggerExample.Create("Licores", new CrearCategoriaDTO
            {
                Nombre = "LIQUORS",
                CategoriaPadre = "ALCOHOLIC"
            });

            yield return SwaggerExample.Create("Bebidas Alcoholicas", new CrearCategoriaDTO
            {
                Nombre = "ALCOHOLIC",
                CategoriaPadre = "DRINKS"
            });

            yield return SwaggerExample.Create("Bebidas sin Alcohol", new CrearCategoriaDTO
            {
                Nombre = "NON_ALCOHOLIC",
                CategoriaPadre = "DRINKS"
            });

            yield return SwaggerExample.Create("Especias", new CrearCategoriaDTO
            {
                Nombre = "SPICES"
            });

        }
    }
}
