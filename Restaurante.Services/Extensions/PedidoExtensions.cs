using Restaurante.Models;

namespace Restaurante.Services
{
    internal static class PedidoExtensions
    {
        public static Pedido DiferenciaDetallePedido(this Pedido pedidoActual, Pedido nuevaInformacion)
        {
            var nPedido = pedidoActual;
            nPedido.Detalles = nuevaInformacion.Detalles.Select(x =>
            {
                //El producto se encuentra.
                var pA = pedidoActual.Detalles.FirstOrDefault(y => y.ProductoId == x.ProductoId);
                if (pA != null)
                {
                    var diff = Math.Abs(pA.Cantidad - x.Cantidad);
                    //Se observa una diferencia.
                    if (diff > 0)
                    {
                        //Se asigna el valor a agregar o a quitar.
                        x.Cantidad = x.Cantidad > pA.Cantidad ? diff : diff * -1;
                        return x;
                    }
                    return null;
                }
                return null;
            })
            .Where(x => x != null)
            .ToList();

            return nPedido;
        }
    }
}
