import angular from "angular";
import ngRoute from "angular-route";
import ngCookies from "angular-cookies";
import ngResource from "angular-resource";
import ngSanitize from "angular-sanitize";
import ngAria from "angular-aria";
import Routes from "@routes";
import Components from "@components";
import Controllers from "@controllers";
import Services from "@services";
import Interceptors from "@interceptors";
import Filters from "@filters";

let RestauranteModule = angular.module("RestauranteModule", [ngRoute, ngCookies, ngResource, ngAria, ngSanitize]);

Services.forEach(([name,service]) => {
    RestauranteModule = RestauranteModule.service(name,service);
});

Controllers.forEach(([name,controller]) => {
    RestauranteModule = RestauranteModule.controller(name,controller);
});

Components.forEach(([name,component]) => {
    RestauranteModule = RestauranteModule.component(name,component);
});

Filters.forEach(([name,filter]) => {
    RestauranteModule = RestauranteModule.filter(name,filter);
});

RestauranteModule = RestauranteModule.config(Routes);
RestauranteModule = RestauranteModule.factory('RequestInterceptor',Interceptors.RequestInterceptor);
RestauranteModule = RestauranteModule.config(['$httpProvider', function($httpProvider, $filterProvider) {
    $httpProvider.interceptors.push('RequestInterceptor');
}]);

export default RestauranteModule;