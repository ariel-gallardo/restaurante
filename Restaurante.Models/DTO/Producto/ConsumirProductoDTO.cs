namespace Restaurante.Models
{
    public class ConsumirProductoDTO
    {
        public class DataDTO
        {
            public string Id { get; set; }
            public int Cantidad { get; set; }
        }
        public IList<DataDTO> Data { get; set; }
    }
}
