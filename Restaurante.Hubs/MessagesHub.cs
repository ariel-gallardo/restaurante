using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Restaurante.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync(MethodsHub.SendOperation, $@"CLIENT_CONNECTED ""Operation,{Context.ConnectionId}""");
            await Clients.Client(Context.ConnectionId).SendAsync(MethodsHub.SendConnectionId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Caller.SendAsync(MethodsHub.SendOperation, $@"CLIENT_DISCONNECTED ""Operation,{Context.ConnectionId}""");
            await base.OnDisconnectedAsync(exception);
        }
        public async Task JoinChannel(string channelId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId);
        }

        public async Task ExitChannel(string channelId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId);
        }
        public string ConnectionId => Context.ConnectionId;
    }
}
