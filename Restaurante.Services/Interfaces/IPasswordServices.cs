using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IPasswordServices
    {
        string Encrypt(string password);
        public bool Ok(string password, string hashPassword);
        public (string,string) GenerateToken(Usuario usuario);
    }
}
