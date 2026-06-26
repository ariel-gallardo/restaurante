import OrderAction from "@models/Order/OrderAction";
import LocalStorageServices from "@services/LocalStorageServices";
import PedidosServices from "@services/PedidosServices";
import { forEach, ILocationService, IRootScopeService } from "angular";

export default class NavBarController {

    static $inject = ['$scope', '$location', 'LocalStorageServices', 'PedidosServices'];

    constructor(private $scope : IRootScopeService, private $location : ILocationService, private LocalStorageServices: LocalStorageServices, private PedidosServices: PedidosServices) {
        this.$scope = $scope;
        this.$location = $location;
        this.verCarrito = this.verCarrito.bind(this);
        this.perfilUsuario = this.perfilUsuario.bind(this)
    }

    public get isV2() {
        return !!(window as any).__V2_RUNNING__;
    }

    public get StatusCss(){
        switch(this.PedidosServices.EstadoPedido){
            case OrderAction.Realizar:
                return 'bg-secondary';
            case OrderAction.Preparar:
                return 'bg-primary';
            case OrderAction.Recepcion:
                return 'bg-info';
            case OrderAction.Delivery:
                return 'bg-warning';
            case OrderAction.Puerta:
                return 'bg-dark';
            case OrderAction.Entregado:
                return 'bg-success';
            case OrderAction.Cancelado:
                return 'bg-danger';
            default: return 'bg-light';
        }
    } 

    public get StatusText(){
        return this.PedidosServices.EstadoPedido;
    }

    public get Count(){
        let data = this.LocalStorageServices.CurrentUser?.pedido?.data;
        let sum = 0;
        if(data){
            forEach(data,x => {
                sum+=x.cantidad;
            });
        }
        return sum;
    }

    verCarrito() {
        this.$location.path('/cart');
    }

    perfilUsuario(){
        this.$location.path('/profile');
    }
}