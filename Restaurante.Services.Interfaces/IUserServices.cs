using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Restaurante.Models;

namespace Restaurante.Services
{
    public interface IUserServices
    {
        void AddHubContext(HubCallerContext context);
        HubCallerContext HubContext {get;}
        IEnumerable<Claim> CurrentUserClaims { get; }
        T CurrentUserClaim<T>(string type);
        public Task<ResultResponse> Register(RegisterDTO dto);
        public Task<ResultResponse> Login(LoginDTO dto);
        public Task<ResultResponse> Info(string token);
        public string CurrentRol { get; }
        public long? CurrentId { get; }
    }
}
