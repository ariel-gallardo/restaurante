import { RestauranteServices } from "@services/RestauranteServices";
import RouteServices from "@services/RouteServices";

export class RestauranteController {
    
    constructor(private RestauranteServices: RestauranteServices, private RouteServices: RouteServices) {
        
    }
}