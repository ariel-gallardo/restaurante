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

export const mount = (containerElement) => {

    let RestauranteModule = angular.module("RestauranteModule", [
        ngRoute, ngCookies, ngResource, ngAria, ngSanitize
    ]);


    Services.forEach(([name, service]) => {
        RestauranteModule.service(name, service);
    });

    Controllers.forEach(([name, controller]) => {
        RestauranteModule.controller(name, controller);
    });

    Components.forEach(([name, component]) => {
        RestauranteModule.component(name, component);
    });

    Filters.forEach(([name, filter]) => {
        RestauranteModule.filter(name, filter);
    });

    RestauranteModule.config(Routes);

    RestauranteModule.config(['$compileProvider', function ($compileProvider) {
        $compileProvider.cssClassDirectivesEnabled(true);
    }]);

    RestauranteModule.factory('RequestInterceptor', Interceptors.RequestInterceptor);

    RestauranteModule.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('RequestInterceptor');
    }]);

    const el = typeof containerElement === 'string'
        ? document.getElementById(containerElement)
        : containerElement;

    if (el) {
        el.innerHTML = `
            <div ng-controller="RestauranteCtrl">
                <nav-bar></nav-bar>
                <div ng-view></div>
                <message></message>
            </div>
        `;
        angular.bootstrap(el, ["RestauranteModule"]);
    } else {
        console.error("No se encontró el contenedor para montar RestauranteModule");
    }
};