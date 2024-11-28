namespace Restaurante.Models
{
    public class PedidoDTO
    {
        public class PedidoDTOData
        {
            public string ProductoId { get; set; }
            public double Cantidad { get; set; }
        }
        public bool Confirmar { get; set; }
        public IList<PedidoDTOData> Data { get; set; }
    }
}
