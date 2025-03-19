namespace Restaurante.Models
{
    public class PosicionDTO
    {
        public string PedidoId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public ushort Velocidad { get; set; }
        public ushort Direccion { get; set; }
        public DateTimeOffset Fecha { get; set; }
    }
}
