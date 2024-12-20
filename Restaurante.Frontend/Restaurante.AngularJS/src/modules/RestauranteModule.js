import angular from "angular";
import ngRoute from "angular-route";

import { RestauranteController } from "../controllers/RestauranteController";
import { RestauranteService } from "../services/RestauranteService";

    const restauranteModule = angular.module("RestauranteModule", [ngRoute])
    .controller("RestauranteCtrl",RestauranteController)
    .service("RestauranteService",RestauranteService)


export default restauranteModule;