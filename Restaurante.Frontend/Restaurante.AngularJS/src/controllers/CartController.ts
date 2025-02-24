import OrderInteraction from "@models/Order/OrderInteraction";
import PedidosServices from "@services/PedidosServices";
import UserServices from "@services/UserServices";
import { IRootScopeService } from "angular";

export default class CartController
{
    static $inject = ['UserServices', '$rootScope', 'PedidosServices'];

    public get Data(){
        return this.UserServices.DetallesPedido;
    }
    
    constructor(private UserServices: UserServices, private $rootScope: IRootScopeService, private PedidosServices: PedidosServices) {
        this.onPlus = this.onPlus.bind(this);
        this.onMinus = this.onMinus.bind(this);
        this.onRemove = this.onRemove.bind(this);
        this.Request = this.Request.bind(this);
        this.Cancel = this.Cancel.bind(this);
    }

    public get EstadoPedido(){
        return this.PedidosServices.EstadoPedido;
    }

    public onPlus(id: string){
        this.$rootScope.$emit('InteractuarCarrito',OrderInteraction.Adicionar(id));
    }

    public onMinus(id: string){
        this.$rootScope.$emit('InteractuarCarrito',OrderInteraction.Quitar(id));
    }

    public onRemove(id: string){
        this.$rootScope.$emit('InteractuarCarrito',OrderInteraction.Remover(id));
    }

    public Request(){
        this.$rootScope.$emit('InteractuarCarrito',OrderInteraction.Realizar())
    }

    public Cancel(){
        this.$rootScope.$emit('InteractuarCarrito',OrderInteraction.Cancelar())
    }
}