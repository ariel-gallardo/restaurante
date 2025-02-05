namespace Restaurante.Models
{
    public class ConsumirProductoDTO
    {
        public class ConsumirProductoDataDTO
        {
            public string Id { get; set; }
            public double Cantidad { get; set; }
        }
        public IList<ConsumirProductoDataDTO> Data { get; set; }
    }
}
