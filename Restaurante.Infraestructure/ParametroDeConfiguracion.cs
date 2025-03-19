namespace Restaurante.Infraestructure
{
    public static class ParametroDeConfiguracion
    {
        public static string[] EstadosPosiblesCliente 
        => new string[] { "SEARCHING", "CREATED", "CANCELLED" };
        public static string[] EstadosPosiblesRecepcionista 
        => new string[] { "CREATED", "PREPAIRING", "RECEPTION", "DELIVERY", "CANCELLED" };
        public static string[] EstadosPosiblesDelivery
        => new string[] { "CLIENT_DOOR", "DONE", "CANCELLED", "DELIVERY" };
        public static string[] EstadosPosiblesCocinero
        => new string[] { "PREPAIRING", "CREATED" };
        public static string[] EstadosPedidoOrdenado 
        => new string[] { "SEARCHING", "CREATED", "PREPAIRING", "RECEPTION", "DELIVERY", "CLIENT_DOOR", "DONE", "CANCELLED" };
    }
}
