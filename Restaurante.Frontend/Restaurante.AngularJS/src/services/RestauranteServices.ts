import MessageServices from "@services/MessageServices";
import PedidosServices from "@services/PedidosServices";
import { IRootScopeService } from "angular";

export class RestauranteServices {    
    static $inject = ['$rootScope', 'PedidosServices', 'MessageServices'];
    constructor(private $rootScope: IRootScopeService, private PedidosServices: PedidosServices, private MessageServices: MessageServices) {
        
    }
}