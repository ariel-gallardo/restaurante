using Restaurante.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API.Examples
{
    public class CrearIngredienteRequestExample : IMultipleExamplesProvider<CrearIngredienteDTO>
    {
        public IEnumerable<SwaggerExample<CrearIngredienteDTO>> GetExamples()
        {
            #region Condimentos
            yield return SwaggerExample.Create("Mayonesa en Sobre", new CrearIngredienteDTO 
            {
                Nombre = "Mayonesa Individual",
                Descripcion = "Sobre 10g",
                PrecioDeCompra = 50,
                PrecioDeVenta = 100,
                StockActual = 500,
                StockAlerta = 50,
                Unidad = "U"
            });

            yield return SwaggerExample.Create("Ketchup en Sobre", new CrearIngredienteDTO
            {
                Nombre = "Ketchup Individual",
                Descripcion = "Sobre 10g",
                PrecioDeCompra = 50,
                PrecioDeVenta = 100,
                StockActual = 200,
                StockAlerta = 50,
                Unidad = "U"
            });

            yield return SwaggerExample.Create("Salsa Golf en Sobre", new CrearIngredienteDTO
            {
                Nombre = "Salsa Golf Individual",
                Descripcion = "Sobre 10g",
                PrecioDeCompra = 50,
                PrecioDeVenta = 100,
                StockActual = 75,
                StockAlerta = 50,
                Unidad = "U"
            });

            yield return SwaggerExample.Create("Mostaza en Sobre", new CrearIngredienteDTO
            {
                Nombre = "Mostaza Individual",
                Descripcion = "Sobre 10g",
                PrecioDeCompra = 50,
                PrecioDeVenta = 100,
                StockActual = 30,
                StockAlerta = 50,
                Unidad = "U"
            });

            yield return SwaggerExample.Create("Picante en Sobre", new CrearIngredienteDTO
            {
                Nombre = "Picante Individual",
                Descripcion = "Sobre 10g",
                PrecioDeCompra = 50,
                PrecioDeVenta = 100,
                StockActual = 55,
                StockAlerta = 50,
                Unidad = "U"
            });
            #endregion

            #region Verduras
            yield return SwaggerExample.Create("Lechuga Repollada", new CrearIngredienteDTO
            {
                Nombre = "Lechuga Repollada",
                PrecioDeCompra = 1200,
                PrecioDeVenta = 2800,
                StockActual = 15,
                StockAlerta = 5,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Lechuga Morada", new CrearIngredienteDTO
            {
                Nombre = "Lechuga Morada",
                PrecioDeCompra = 700,
                PrecioDeVenta = 1400,
                StockActual = 2,
                StockAlerta = 3,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Tomate Perita", new CrearIngredienteDTO
            {
                Nombre = "Tomate Perita",
                PrecioDeCompra = 1500,
                PrecioDeVenta = 3000,
                StockActual = 8,
                StockAlerta = 6,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Jamon Cocido", new CrearIngredienteDTO
            {
                Nombre = "Jamon Cocido",
                PrecioDeCompra = 13000,
                PrecioDeVenta = 26000,
                StockActual = 13,
                StockAlerta = 10,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Queso Muzarella", new CrearIngredienteDTO
            {
                Nombre = "Queso Muzarella",
                PrecioDeCompra = 12000,
                PrecioDeVenta = 25000,
                StockActual = 5,
                StockAlerta = 6,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Queso en fetas", new CrearIngredienteDTO
            {
                Nombre = "Queso en fetas",
                PrecioDeCompra = 16000,
                PrecioDeVenta = 24000,
                StockActual = 9,
                StockAlerta = 3,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Aceituna", new CrearIngredienteDTO
            {
                Nombre = "Aceitunas",
                PrecioDeCompra = 2500,
                PrecioDeVenta = 2900,
                StockActual = 9,
                StockAlerta = 3,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Papa", new CrearIngredienteDTO
            {
                Nombre = "Papa",
                PrecioDeCompra = 2000,
                PrecioDeVenta = 4000,
                StockActual = 35,
                StockAlerta = 20,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Cebolla Morada", new CrearIngredienteDTO
            {
                Nombre = "Cebolla Morada",
                PrecioDeCompra = 1450,
                PrecioDeVenta = 2800,
                StockActual = 18,
                StockAlerta = 10,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Aceite Girasol", new CrearIngredienteDTO
            {
                Nombre = "Aceite Girasol",
                PrecioDeCompra = 5000,
                PrecioDeVenta = 8000,
                StockActual = 100,
                StockAlerta = 20,
                Unidad = "L"
            });
            yield return SwaggerExample.Create("Aceite de Oliva", new CrearIngredienteDTO
            {
                Nombre = "Aceite de Oliva",
                PrecioDeCompra = 7000,
                PrecioDeVenta = 10000,
                StockActual = 28,
                StockAlerta = 5,
                Unidad = "L"
            });
            yield return SwaggerExample.Create("Sal", new CrearIngredienteDTO
            {
                Nombre = "Sal",
                PrecioDeCompra = 500,
                PrecioDeVenta = 800,
                StockActual = 10,
                StockAlerta = 5,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Vinagre de vino", new CrearIngredienteDTO
            {
                Nombre = "Vinagre de vino",
                PrecioDeCompra = 850,
                PrecioDeVenta = 1100,
                StockActual = 16,
                StockAlerta = 5,
                Unidad = "L"
            });
            yield return SwaggerExample.Create("Jugo de Limon", new CrearIngredienteDTO
            {
                Nombre = "Jugo de Limon",
                Descripcion = "Ensaladas",
                PrecioDeCompra = 2000,
                PrecioDeVenta = 2900,
                StockActual = 10,
                StockAlerta = 3,
                Unidad = "L"
            });
            yield return SwaggerExample.Create("Harina", new CrearIngredienteDTO
            {
                Nombre = "Harina",
                PrecioDeCompra = 1200,
                PrecioDeVenta = 2600,
                StockActual = 50,
                StockAlerta = 10,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Pimienta molida", new CrearIngredienteDTO
            {
                Nombre = "Pimienta molida",
                PrecioDeCompra = 3000,
                PrecioDeVenta = 3500,
                StockActual = 5,
                StockAlerta = 1.5,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Oregano fresco", new CrearIngredienteDTO
            {
                Nombre = "Oregano fresco",
                PrecioDeCompra = 2500,
                PrecioDeVenta = 2900,
                StockActual = 6,
                StockAlerta = 4,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Ajo", new CrearIngredienteDTO
            {
                Nombre = "Ajo",
                PrecioDeCompra = 1200,
                PrecioDeVenta = 1800,
                StockActual = 1.5,
                StockAlerta = 2.5,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Huevo", new CrearIngredienteDTO
            {
                Nombre = "Huevo",
                PrecioDeCompra = 80,
                PrecioDeVenta = 120,
                StockActual = 126,
                StockAlerta = 50,
                Unidad = "U"
            });
            yield return SwaggerExample.Create("Pimiento Rojo", new CrearIngredienteDTO
            {
                Nombre = "Pimiento Rojo",
                PrecioDeCompra = 1100,
                PrecioDeVenta = 1800,
                StockActual = 45,
                StockAlerta = 30,
                Unidad = "U"
            });
            yield return SwaggerExample.Create("Palta", new CrearIngredienteDTO
            {
                Nombre = "Palta",
                PrecioDeCompra = 400,
                PrecioDeVenta = 850,
                StockActual = 43,
                StockAlerta = 30,
                Unidad = "U"
            });
            #endregion

            #region Carnes
            yield return SwaggerExample.Create("Bola de lomo", new CrearIngredienteDTO
            {
                Nombre = "Bola de lomo",
                PrecioDeCompra = 7000,
                PrecioDeVenta = 12000,
                StockActual = 12,
                StockAlerta = 8,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Molida de pollo", new CrearIngredienteDTO
            {
                Nombre = "Molida de pollo",
                PrecioDeCompra = 3000,
                PrecioDeVenta = 6500,
                StockActual = 23,
                StockAlerta = 10,
                Unidad = "KG"
            });
            yield return SwaggerExample.Create("Grasa", new CrearIngredienteDTO
            {
                Nombre = "Grasa",
                PrecioDeCompra = 500,
                PrecioDeVenta = 700,
                StockActual = 10,
                StockAlerta = 6,
                Unidad = "KG"
            });
            #endregion
        }
    }
}
