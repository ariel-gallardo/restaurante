import OrderAction from "@models/Order/OrderAction";
import LocalStorageServices from "@services/LocalStorageServices";
import PedidosServices from "@services/PedidosServices";
import { forEach, ILocationService, IRootScopeService } from "angular";
import { TRANSLATIONS, setSharedTheme, setSharedLanguage } from "@org/shared-shell";

export default class NavBarController {
    static $inject = ['$scope', '$location', 'LocalStorageServices', 'PedidosServices', '$ngRedux'];
    private unsubscribeRedux: (() => void) | null = null;
    public currentTheme: 'light' | 'dark' = 'light';
    public currentLanguage: 'es' | 'en' = 'es';
    public isMenuCollapsed = true;

    constructor(
        private $scope: IRootScopeService,
        private $location: ILocationService,
        private LocalStorageServices: LocalStorageServices,
        private PedidosServices: PedidosServices,
        private $ngRedux: any
    ) {
        this.verCarrito = this.verCarrito.bind(this);
        this.perfilUsuario = this.perfilUsuario.bind(this);
        this.toggleTheme = this.toggleTheme.bind(this);
        this.setLanguage = this.setLanguage.bind(this);
        this.toggleMenu = this.toggleMenu.bind(this);

        const mapStateToThis = (state: any) => ({
            currentTheme: state?.orderState?.theme || 'light',
            currentLanguage: state?.orderState?.language || 'es'
        });
        this.unsubscribeRedux = this.$ngRedux.connect(mapStateToThis)(this);
    }

    public get isV2() {
        return !!(window as any).__V2_RUNNING__;
    }

    public get t() {
        const lang = this.currentLanguage || 'es';
        return TRANSLATIONS[lang] || TRANSLATIONS.es;
    }

    public get StatusCss(){
        switch(this.PedidosServices.EstadoPedido){
            case OrderAction.Buscar:
                return 'badge-searching';
            case OrderAction.Realizar:
                return 'badge-created';
            case OrderAction.Preparar:
                return 'badge-preparing';
            case OrderAction.Recepcion:
                return 'badge-reception';
            case OrderAction.Delivery:
                return 'badge-delivery';
            case OrderAction.Puerta:
                return 'badge-door';
            case OrderAction.Entregado:
                return 'badge-done';
            case OrderAction.Cancelar:
            case OrderAction.Cancelado:
                return 'badge-canceled';
            default: return 'badge-searching';
        }
    } 

    public get StatusText(){
        const status = this.PedidosServices.EstadoPedido;
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
        this.isMenuCollapsed = true;
        this.$location.path('/cart');
    }

    perfilUsuario(){
        this.isMenuCollapsed = true;
        this.$location.path('/profile');
    }

    toggleTheme() {
        const nextTheme = this.currentTheme === 'dark' ? 'light' : 'dark';
        this.$ngRedux.dispatch(setSharedTheme({ theme: nextTheme }));
    }

    setLanguage(lang: 'es' | 'en') {
        this.$ngRedux.dispatch(setSharedLanguage({ language: lang }));
    }

    toggleMenu() {
        this.isMenuCollapsed = !this.isMenuCollapsed;
    }

    public $onDestroy(){
        if(this.unsubscribeRedux)
            this.unsubscribeRedux();
    }
}