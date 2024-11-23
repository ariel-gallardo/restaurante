using Restaurante.Models;
using UnitsNet.Units;
using UnitsNet;

namespace Restaurante.Services
{
    internal static class ProductoExtensions
    {
        public static (string,double?) VerConsumoIndividualDeIngrediente(this ProductoIngrediente pI)
        {
            var iBase = pI.Ingrediente;
            double? cValue = iBase.StockActual ?? 0.0;
            switch (pI.Unidad)
            {
                case "G":
                    if (iBase.Unidad == "G")
                        cValue = cValue.Value - pI.Cantidad;
                    else if (iBase.Unidad == "KG")
                        cValue = cValue.Value - UnitConverter.Convert(pI.Cantidad, MassUnit.Gram, MassUnit.Kilogram);                
                    break;
                case "ML":
                    if (iBase.Unidad == "ML")
                        cValue = cValue.Value - pI.Cantidad;
                    else if (iBase.Unidad == "L")
                        cValue = cValue.Value - UnitConverter.Convert(pI.Cantidad, VolumeUnit.Milliliter, VolumeUnit.Liter);
                    break;
                case "KG":
                    if (iBase.Unidad == "KG")
                        cValue = cValue.Value - pI.Cantidad;
                    else if (iBase.Unidad == "G")
                        cValue = cValue.Value - UnitConverter.Convert(pI.Cantidad, MassUnit.Kilogram, MassUnit.Gram);
                    break;
                case "L":
                    if (iBase.Unidad == "L")
                        cValue = cValue.Value - pI.Cantidad;
                    else if (iBase.Unidad == "ML")
                        cValue = cValue.Value - UnitConverter.Convert(pI.Cantidad, VolumeUnit.Liter, VolumeUnit.Milliliter);
                    break;
                case "U":
                    cValue = cValue.Value - pI.Cantidad;
                    break;
                default:
                    cValue = null;
                    break;
            }
            return (pI.IngredienteId,cValue);
        }

        /// <summary>
        /// Id (Ingrediente|Producto) | (Cantidad, Ingrediente o Producto) 
        /// </summary>
        /// <param name="productos"></param>
        /// <returns></returns>
        public static IDictionary<string,(double,bool)> VerConsumoIndividualDeProductos(this IList<Producto> productos)
        {
            IDictionary<string,(double,bool)> consumoDeProducto = new Dictionary<string,(double,bool)>();
            foreach(var p in productos)
            {
                var hasIngredients = p.Ingredientes.Count > 0;
                if(hasIngredients)
                foreach (var i in p.Ingredientes) 
                {
                    (var id, var consumo) = i.VerConsumoIndividualDeIngrediente();
                    consumoDeProducto.Add(id,(consumo.HasValue ? consumo.Value : -1,hasIngredients));
                }
                else
                {
                    consumoDeProducto.Add(p.Id, (1, hasIngredients));
                }
            }
            var iNoUnit = consumoDeProducto.Where(x => x.Value.Item1 == -1);
            if (iNoUnit.Count() > 0)
                throw new Exception($@"CANNOT_CONVERT_INGREDIENTS_NO_UNIT ""{string.Join(",", iNoUnit.Select(x => x.Key))}""");
            return consumoDeProducto;
        }

        /// <summary>
        /// Id (Ingrediente|Producto) | (Cantidad, Ingrediente o Producto) 
        /// </summary>
        /// <param name="productos"></param>
        /// <returns></returns>
        public static IDictionary<string, (double, bool)> VerConsumoDeProductos(this IEnumerable<Producto> productos, ConsumirProductoDTO dto)
        {
            IDictionary<string, (double, bool)> consumoDeProducto = new Dictionary<string, (double, bool)>();
            foreach (var p in productos)
            {
                var hasIngredients = p.Ingredientes.Count > 0;
                if (hasIngredients)
                    foreach (var i in p.Ingredientes)
                    {
                        (var id, var consumo) = i.VerConsumoIndividualDeIngrediente();
                        consumoDeProducto.Add(id, (consumo.HasValue ? (consumo.Value * dto.Data.FirstOrDefault(x => x.Id == p.Id).Cantidad) : -1, hasIngredients));
                    }
                else
                {
                    consumoDeProducto.Add(p.Id, (1 * dto.Data.FirstOrDefault(x => x.Id == p.Id).Cantidad, hasIngredients));
                }
            }
            var iNoUnit = consumoDeProducto.Where(x => x.Value.Item1 == -1);
            if (iNoUnit.Count() > 0)
                throw new Exception($@"CANNOT_CONVERT_INGREDIENTS_NO_UNIT ""{string.Join(",", iNoUnit.Select(x => x.Key))}""");
            return consumoDeProducto;
        }
    }
}
