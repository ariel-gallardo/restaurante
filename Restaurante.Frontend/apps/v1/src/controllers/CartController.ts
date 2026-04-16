import OrderInteraction from "@models/Order/OrderInteraction";
import ORDER_STATUS from "@models/Order/OrderStatus";
import PedidosServices from "@services/PedidosServices";
import UserServices from "@services/UserServices";
import { IRootScopeService } from "angular";

export default class CartController
{
    static $inject = ['UserServices', '$rootScope', 'PedidosServices', 'ORDER_STATUS', '$ngRedux'];
    public readonly ORDER_STATUS;
    private unsubscribeRedux: (() => void) | null = null;
    private reduxOrderStatus: string = '';

    public get Data(){
        return this.UserServices.DetallesPedido;
    }
    
    constructor(private UserServices: UserServices, private $rootScope: IRootScopeService, private PedidosServices: PedidosServices, ORDER_STATUS_CONST: typeof ORDER_STATUS, private $ngRedux: any) {
        this.ORDER_STATUS = ORDER_STATUS_CONST;
        this.onPlus = this.onPlus.bind(this);
        this.onMinus = this.onMinus.bind(this);
        this.onRemove = this.onRemove.bind(this);
        this.Request = this.Request.bind(this);
        this.Cancel = this.Cancel.bind(this);

        const mapStateToThis = (state: any) => ({
            reduxOrderStatus: state?.orderState?.ORDER_STATUS || this.ORDER_STATUS.CREATED,
        });
        this.unsubscribeRedux = this.$ngRedux.connect(mapStateToThis)(this);
    }

    public get EstadoPedido(){
        return this.reduxOrderStatus || this.PedidosServices.EstadoPedido;
    }

    public get IsSearching(){
        return this.EstadoPedido === this.ORDER_STATUS.SEARCHING;
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

    public $onDestroy(){
        if(this.unsubscribeRedux)
            this.unsubscribeRedux();
    }
}