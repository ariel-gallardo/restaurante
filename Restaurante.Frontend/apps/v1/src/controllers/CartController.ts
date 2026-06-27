import OrderInteraction from "@models/Order/OrderInteraction";
import ORDER_STATUS from "@models/Order/OrderStatus";
import PedidosServices from "@services/PedidosServices";
import UserServices from "@services/UserServices";
import { IRootScopeService } from "angular";
import { TRANSLATIONS } from "@org/shared-shell";

export default class CartController
{
    static $inject = ['UserServices', '$rootScope', 'PedidosServices', 'ORDER_STATUS', '$ngRedux'];
    public readonly ORDER_STATUS;
    private unsubscribeRedux: (() => void) | null = null;
    private reduxOrderStatus: string = '';
    public currentLanguage: 'es' | 'en' = 'es';

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
            currentLanguage: state?.orderState?.language || 'es',
        });
        this.unsubscribeRedux = this.$ngRedux.connect(mapStateToThis)(this);
    }

    public get EstadoPedido(){
        return this.reduxOrderStatus || this.PedidosServices.EstadoPedido;
    }

    public get TranslatedEstadoPedido() {
        const status = this.EstadoPedido;
        const keysMap: Record<string, string> = {
            'SEARCHING': 'SEARCHING',
            'CREATED': 'CREATED',
            'PREPAIRING': 'PREPAIRING',
            'RECEPTION': 'RECEPTION',
            'DELIVERY': 'DELIVERY',
            'CLIENT_DOOR': 'CLIENT_DOOR',
            'DONE': 'DONE',
            'CANCEL': 'CANCELED',
            'CANCELED': 'CANCELED'
        };
        const mappedKey = keysMap[status] || status;
        return this.t[mappedKey] || status;
    }

    public get t() {
        const lang = this.currentLanguage || 'es';
        return TRANSLATIONS[lang] || TRANSLATIONS.es;
    }

    public get IsSearching(){
        return this.EstadoPedido === this.ORDER_STATUS.SEARCHING;
    }

    public get totalAmount() {
        // Since we don't have price info in Data, we can sum the quantity or show total item count
        let sum = 0;
        if (this.Data) {
            for (const x of this.Data) {
                sum += x.cantidad || 0;
            }
        }
        return sum;
    }

    public get getStatusBadgeClass() {
        switch(this.EstadoPedido){
            case 'SEARCHING':
                return 'bg-secondary';
            case 'CREATED':
                return 'bg-primary';
            case 'PREPAIRING':
                return 'bg-info text-dark';
            case 'RECEPTION':
                return 'bg-warning text-dark';
            case 'DELIVERY':
                return 'bg-dark';
            case 'CLIENT_DOOR':
                return 'bg-info';
            case 'DONE':
                return 'bg-success';
            case 'CANCEL':
            case 'CANCELED':
                return 'bg-danger';
            default: return 'bg-light text-dark';
        }
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