using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Restaurante.Hubs;

namespace Restaurante.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly IHubContext<MessagesHub> _hubContext;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public MessageServices(IHubContext<MessagesHub> hubContext, IHttpContextAccessor httpContextAccessor) 
        {
            _hubContext = hubContext;
            _httpContextAccesor = httpContextAccessor;
        }

        private string HttpGroupId { get
            {
                string groupId = null;
                var ctx = _httpContextAccesor.HttpContext;
                if (ctx != null)
                {
                    if (ctx.Request.Headers.ContainsKey("GroupId"))
                        groupId = ctx.Request.Headers["GroupId"];
                }
                return groupId;
            } 
        }

        public string GroupId { get; set; }

        public async Task<bool> SendMessage(string message, int statusCode)
        {
            if (!string.IsNullOrEmpty(GroupId) || !string.IsNullOrEmpty(HttpGroupId))
            {
                await _hubContext.Clients.Group(GroupId ?? HttpGroupId).SendAsync(MethodsHub.SendMessage, new {
                    message,
                    statusCode
                });
                return true;
            }
            return false;
        }

        public async Task<bool> SendOperation(string operation)
        {
            if (!string.IsNullOrEmpty(GroupId) || !string.IsNullOrEmpty(HttpGroupId))
            {
                await _hubContext.Clients.Group(GroupId ?? HttpGroupId).SendAsync(MethodsHub.SendOperation, operation);
                return true;
            }
            return false;
        }
    }
}
