import { RestauranteServices } from "@services/RestauranteServices";
import RouteServices from "@services/RouteServices";
import ORDER_STATUS from "@models/Order/OrderStatus";

export class RestauranteController {
    static $inject = ['RestauranteServices', 'RouteServices', 'ORDER_STATUS', '$ngRedux'];
    public readonly ORDER_STATUS;
    private unsubscribeRedux: (() => void) | null = null;
    
    constructor(
        private RestauranteServices: RestauranteServices,
        private RouteServices: RouteServices,
        ORDER_STATUS_CONST: typeof ORDER_STATUS,
        private $ngRedux: any
    ) {
        this.ORDER_STATUS = ORDER_STATUS_CONST;
        
        const mapStateToThis = (state: any) => {
            const theme = state?.orderState?.theme || 'light';
            return { theme };
        };

        this.unsubscribeRedux = this.$ngRedux.connect(mapStateToThis)((stateAttrs: any) => {
            this.applyTheme(stateAttrs.theme);
        });
    }

    public get isV2() {
        return !!(window as any).__V2_RUNNING__;
    }

    private applyTheme(theme: 'light' | 'dark') {
        if (theme === 'dark') {
            document.body.classList.add('dark-theme');
            document.body.classList.remove('light-theme');
        } else {
            document.body.classList.add('light-theme');
            document.body.classList.remove('dark-theme');
        }
    }

    public $onDestroy() {
        if (this.unsubscribeRedux) {
            this.unsubscribeRedux();
        }
    }
}