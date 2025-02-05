namespace Restaurante.Models.Enums
{
    public static class EstadoPedido
    {
        public const string Buscando = "SEARCHING";
        public const string Realizado = "CREATED";
        public const string Preparando = "PREPAIRING";
        public const string Delivery = "DELIVERY";
        public const string Puerta = "CLIENT_DOOR";
        public const string Recepcion = "RECEPTION";
        public const string Entregado = "DONE";
        public const string Cancelado = "CANCELLED";
    }
}
