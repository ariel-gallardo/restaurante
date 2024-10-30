namespace Restaurante.Models.Enums
{
    public enum EstadoPedido
    {
        Buscando,        // El cliente está buscando en la tienda
        PedidoRealizado, // El cliente ha realizado un pedido
        EnPreparacion,   // El pedido está siendo preparado por el cocinero
        ListoParaEntregar, // El pedido está listo para ser entregado
        Entregado,       // El pedido ha sido entregado al cliente
        Cancelado        // El pedido ha sido cancelado
    }
}
