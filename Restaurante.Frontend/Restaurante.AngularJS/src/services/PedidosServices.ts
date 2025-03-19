import { HttpTransportType, HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import Order from "@models/Order/Order";
import OrderAction from "@models/Order/OrderAction";
import OrderDetail from "@models/Order/OrderDetail";
import OrderInteraction from "@models/Order/OrderInteraction";
import Pagination from "@models/Pagination";
import PosicionDTO from "@models/Posicion/PosicionDTO";
import ProductStoreMin from "@models/Product/ProductStoreMin";
import Response from "@models/Response";
import OrderGetQuerie from "@queries/OrderGetQuerie";
import ApiServices from "@services/ApiServices";
import EnvironmentServices from "@services/EnvironmentServices";
import LocalStorageServices from "@services/LocalStorageServices";
import UserServices from "@services/UserServices";
import { cookies, IAngularEvent, IRootScopeService, IScope } from "angular";

export default class PedidosServices{
    static $inject = ['$rootScope','UserServices', 'EnvironmentServices', 'LocalStorageServices', '$cookies', 'ApiServices'];
    private _pedidosHub : HubConnection;
    private _smsId: string;
    private _n: Navigator;
    private _check: boolean = false;
    private _url: string = '/api/pedido';

    constructor(private $rootScope: IRootScopeService,
        private UserServices: UserServices, 
        private EnvironmentServices: EnvironmentServices, 
        private LocalStorageServices: LocalStorageServices, 
        private $cookies: cookies.ICookiesService,
        private ApiServices: ApiServices) {
        this._n = window.navigator;
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
        this.$rootScope.$on('Delivery_Select',this.Select.bind(this));
        this.$rootScope.$on('Delivery_StopCheckPosition',this.StopCheckPosition.bind(this));
        this.GetByQuerie.bind(this);
    }


    public get PedidosHub(){
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
            });
            this._pedidosHub.on('Status', (nuevoEstado: string) => {
                this.LocalStorageServices.CurrentUser.pedido.estado = nuevoEstado;
            });
            
            this._pedidosHub.on('Assign', async (pedidoId: string, deliveryId: string, clienteId: string) => {
                if(this.UserServices.Id == deliveryId &&
                    !this.LocalStorageServices.CurrentUser.pedidoTrabajo.find(x => x.pedido == pedidoId)
                )
                {
                    let {content} = await this.ApiServices.get<Response<Order>>(`/api/pedidos?id=${pedidoId}`);
                    if(content != null){
                        this.LocalStorageServices.CurrentUser.pedidoTrabajo=
                        [...this.LocalStorageServices.CurrentUser.pedidoTrabajo,
                            content as Order
                        ];
                        this._check = true;
                        this.SetPosition();
                    }

                }else if(this.UserServices.Id == clienteId){
                     let pos = new PosicionDTO;
                     const [lat, lng] = this.EnvironmentServices.RestaurantePosition;
                     pos.Latitud = lat;
                     pos.Longitud = lng;
                     this.UserServices.PosicionDelivery = pos;
                }
            });

            this._pedidosHub.on('Posicion', (pos: PosicionDTO) => {
                this.UserServices.PosicionDelivery = pos;
                this.$rootScope.$emit('Map_Move',pos);
            });
        }
        return this._pedidosHub;
    }

    //Recepcionista
    private Select(e: IAngularEvent, orderId:string, clientId: string, deliveryId: string){
        this._pedidosHub.invoke('SeleccionarDelivery',orderId, clientId, deliveryId);
    }

    //Sistema
    private StopCheckPosition(){
        this._check = false;
        this.Disconnect();
    }

    //Delivery
    private SetPosition(){
        this._n.geolocation.getCurrentPosition(s => {
            let p = this.UserServices.PedidosTrabajo[0];
            if(this._check && this.UserServices.Rol == 'Delivery' && p){
                this._pedidosHub.invoke('Posicionar',PosicionDTO.FromGeoLocation(s,p.pedido, this.UserServices.Id));
            }
        });
    }

    public async GetByQuerie(querie: OrderGetQuerie) : Promise<Response<Pagination<Order>>>{
        return await this.ApiServices.get(`${this._url}${QuerieURLFromObject(querie)}`);
    }

    public Disconnect(){
        this._pedidosHub.stop();
        this._pedidosHub = null;
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
                    || pedido.Action != OrderAction.Quitar))
                pedido.estado = i.Action;send = true;
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