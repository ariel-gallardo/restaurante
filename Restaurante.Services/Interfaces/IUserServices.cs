using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IUserServices
    {
        public Task<ResultResponse> Register(RegisterDTO dto);
        public Task<ResultResponse> Login(LoginDTO dto);
        public Task<ResultResponse> Info(string token);
    }
}
