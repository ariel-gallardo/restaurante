using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Restaurante.Models;
using Restaurante.Services;

namespace Restaurante.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PedidoHub : Hub
    {
        private readonly IPedidoServices _pServices;
        private readonly IUserServices _uServices;
        private readonly IHubContext<MessagesHub> _mHub;

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync(MethodsHub.SendOperation, $@"CLIENT_CONNECTED ""Pedido,{Context.ConnectionId}""");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Caller.SendAsync(MethodsHub.SendOperation, $@"CLIENT_DISCONNECTED ""Pedido,{Context.ConnectionId}""");
            await base.OnDisconnectedAsync(exception);
        }

        public PedidoHub(IPedidoServices pServices, IUserServices uServices, IHubContext<MessagesHub> mHub)
        {
            _pServices = pServices;
            _uServices = uServices;
            _mHub = mHub;
        }
        public async Task JoinPedidoGroup(string pedidoId, string smsGroupId)
        {
            _pServices.SmsGroupId = smsGroupId;
            _uServices.AddHubContext(Context);
            await _pServices.ObservarPedido(pedidoId);
        }

        public async Task Interactuar(PedidoDTO dto, string smsGroupId)
        {
            _pServices.SmsGroupId = smsGroupId;
            _uServices.AddHubContext(Context);
            await _pServices.InteractuarPedidoActual(dto);
        }

        public async Task ExitPedidoGroup(string pedidoId, string smsGroupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, pedidoId);
            await _mHub.Clients.Group(smsGroupId).SendAsync(MethodsHub.SendOperation, $@"ORDER_DISCONNECTED ""{pedidoId}|{Context.ConnectionId}""");
        }
    }
}
