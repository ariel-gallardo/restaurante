import { HttpTransportType, HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import Order from "@models/Order/Order";
import OrderAction from "@models/Order/OrderAction";
import OrderDetail from "@models/Order/OrderDetail";
import OrderInteraction from "@models/Order/OrderInteraction";
import ProductStoreMin from "@models/Product/ProductStoreMin";
import ORDER_STATUS, { OrderStatus } from "@models/Order/OrderStatus";
import EnvironmentServices from "@services/EnvironmentServices";
import LocalStorageServices from "@services/LocalStorageServices";
import { setOrderStatus } from "@store/orderStatusStore";
import UserServices from "@services/UserServices";
import { cookies, IRootScopeService } from "angular";

export default class PedidosServices{
    static $inject = ['$rootScope', 'UserServices', 'EnvironmentServices', 'LocalStorageServices', '$cookies', '$ngRedux'];
    private _pedidosHub : HubConnection;
    private _smsId: string;

    constructor(private $rootScope: IRootScopeService, private UserServices: UserServices, private EnvironmentServices: EnvironmentServices, private LocalStorageServices: LocalStorageServices, private $cookies: cookies.ICookiesService, private $ngRedux: any) {
        this.$rootScope.$on('InteractuarCarrito', (e,i) => {
            this.InteractuarCarrito(i);
        });
        this.$rootScope.$on('CheckOrder', async () => {
            try{
                if(this.PedidosHub.state != HubConnectionState.Connected)
                    await this.PedidosHub.start();
                if(this.PedidosHub.state == HubConnectionState.Connected && this._smsId)
                    await this.PedidosHub.invoke('JoinPedidoGroup',this.PedidoId,this._smsId);
            }catch(e){
                console.log(e);
            }
        });
        this.$rootScope.$on('ConnectionId', (e,id:string) => {this._smsId = id;});
        this.pushOrderStatusToRedux(ORDER_STATUS.CREATED);
    }


    private get PedidosHub(){
        if(this._pedidosHub == null){
            let token = this.UserServices.Token.replace('Bearer ','');
            this._pedidosHub = new HubConnectionBuilder()
            .withUrl(this.EnvironmentServices.WsAddressPedido, {
                accessTokenFactory: () => token,
                withCredentials: true,
                transport: HttpTransportType.LongPolling
            })
            .withAutomaticReconnect()
            .configureLogging(LogLevel.None)
            .build();
            //this._pedidosHub.on('Message',(m:string) => console.log(`Message ${m}`));
            //this._pedidosHub.on('Operation',(m:string) => console.log(`Operation ${m}`));
            this._pedidosHub.on('Order', (order: any) => {
                this.LocalStorageServices.CurrentUser.pedido = order;
                this.pushOrderStatusToRedux(order?.estado as OrderStatus);
                this.pushCartCountToRedux(order);
            });
        }
        return this._pedidosHub;
    }

    private pushCartCountToRedux(order: any){
        let count = 0;
        if(order && order.data){
            for(let i=0; i<order.data.length; i++){
                count += order.data[i].cantidad || 0;
            }
        }
        this.$ngRedux.dispatch({ type: 'CART/SET_COUNT', payload: count });
    }

    private pushOrderStatusToRedux(status: OrderStatus){
        if(status)
            this.$ngRedux.dispatch(setOrderStatus(status));
    }

    public get DetallesPedido(){
        return this.UserServices.DetallesPedido;
    }

    public get Pedido(){
        return this.UserServices.Pedido;
    }

    public get EstadoPedido(){
        return this.Pedido.estado;
    }

    public get PedidoId(){
        return this.Pedido.pedido;
    }

    private async InteractuarCarrito<T>(i: OrderInteraction<T>){
        let t = this.UserServices.Token;
        let send = false;
        let pedido = JSON.parse(JSON.stringify(this.Pedido));
        let detalle_pedido = pedido.data;
        switch(i.Action){
            case OrderAction.Adicionar:
                let dataA = i.Data as ProductStoreMin;
                let cEA = detalle_pedido.find(x => x.productoId == dataA.id);
                if(cEA) detalle_pedido[detalle_pedido.indexOf(cEA)].cantidad++;
                else {
                    let oDA = new OrderDetail;
                    oDA.cantidad = 1;
                    oDA.productoId = dataA.id;
                    detalle_pedido.push(oDA);
                }
                send = true;
                break;
            case OrderAction.Quitar:
                let dataB = i.Data as ProductStoreMin;
                let cEB = detalle_pedido.find(x => x.productoId == dataB.id);
                if(cEB && cEB.cantidad-1 > -1){
                    send = true;
                    if(cEB.cantidad-1 == 0)
                        detalle_pedido.splice(detalle_pedido.indexOf(cEB),1);
                    else
                    detalle_pedido[detalle_pedido.indexOf(cEB)].cantidad--;
                }
                break;
            case OrderAction.Remover:
                let dataC = i.Data as ProductStoreMin;
                let cEC = detalle_pedido.find(x => x.productoId == dataC.id);
                if(cEC){
                    send = true;
                    detalle_pedido = [...detalle_pedido.filter(x => x != cEC)];
                }
                break;
            default:
                if(i.Action != pedido.Action && 
                    (pedido.Action != OrderAction.Adicionar 
                    || pedido.Action != OrderAction.Remover 
                    || pedido.Action != OrderAction.Quitar)) {
                    pedido.estado = i.Action;
                    send = true;
                    this.pushOrderStatusToRedux(i.Action as OrderStatus);
                }
                break;   
        }
        if(send && this.UserServices.TiempoExpiracionToken != '-')
        {
            try{
                if(this.PedidosHub.state != HubConnectionState.Connected)
                    await this.PedidosHub.start();
                if(this.PedidosHub.state == HubConnectionState.Connected && this._smsId)
                {
                    pedido.data = detalle_pedido;
                    await this.PedidosHub.invoke('Interactuar',pedido,this._smsId);
                }
            }catch(e){
                
            }
        }
    }
}