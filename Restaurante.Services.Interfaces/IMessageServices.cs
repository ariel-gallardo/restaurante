namespace Restaurante.Services
{
    public interface IMessageServices
    {
        string GroupId { get; set; }
        Task<bool> SendMessage(string message, int statusCode);
        Task<bool> SendOperation(string operation);
    }
}
