import angular from "angular";
import ngRoute from "angular-route";
import Routes from "@routes";
import Components from "@components";
import Controllers from "@controllers";
import Services from "@services";

const RestauranteModule = angular.module("RestauranteModule", [ngRoute]);

Services.forEach(([name,service]) => {
    RestauranteModule.service(name,service);
});

Controllers.forEach(([name,controller]) => {
    RestauranteModule.controller(name,controller);
});

Components.forEach(([name,component]) => {
    RestauranteModule.component(name,component);
});

RestauranteModule.config(Routes);

export default RestauranteModule;