namespace Restaurante.Models
{
    public class ConsumirProductoDTO
    {
        public class ConsumirProductoDataDTO
        {
            public string Id { get; set; }
            public int Cantidad { get; set; }
        }
        public IList<ConsumirProductoDataDTO> Data { get; set; }
    }
}
