import { RestauranteServices } from "@services/RestauranteServices";
import RouteServices from "@services/RouteServices";
import ORDER_STATUS from "@models/Order/OrderStatus";

export class RestauranteController {
    static $inject = ['RestauranteServices', 'RouteServices', 'ORDER_STATUS'];
    public readonly ORDER_STATUS;
    
    constructor(private RestauranteServices: RestauranteServices, private RouteServices: RouteServices, ORDER_STATUS_CONST: typeof ORDER_STATUS) {
        this.ORDER_STATUS = ORDER_STATUS_CONST;
        
    }
}