import angular from "angular";
import ngRoute from "angular-route";
import ngCookies from "angular-cookies";
import Routes from "@routes";
import Components from "@components";
import Controllers from "@controllers";
import Services from "@services";
import Interceptors from "@interceptors";

const RestauranteModule = angular.module("RestauranteModule", [ngRoute, ngCookies]);

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
RestauranteModule.factory('RequestInterceptor',Interceptors.RequestInterceptor);
RestauranteModule.config(['$httpProvider', function($httpProvider) {
    $httpProvider.interceptors.push('RequestInterceptor');
}]);

export default RestauranteModule;