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

const getPublicBaseUrl = () => {
    const value = process.env.LEGACY_PUBLIC_BASE_URL || window.location.origin;
    return value.replace(/\/$/, '');
};

const ensureLink = (id, href) => {
    if (document.getElementById(id)) {
        return;
    }

    const link = document.createElement('link');
    link.id = id;
    link.rel = 'stylesheet';
    link.href = href;
    document.head.appendChild(link);
};

const ensureScript = (id, src) => {
    if (document.getElementById(id)) {
        return Promise.resolve();
    }

    return new Promise((resolve, reject) => {
        const script = document.createElement('script');
        script.id = id;
        script.src = src;
        script.onload = () => resolve();
        script.onerror = () => reject(new Error(`No se pudo cargar ${src}`));
        document.head.appendChild(script);
    });
};

export const mount = async (containerElement) => {
    const publicBaseUrl = getPublicBaseUrl();

    ensureLink('legacy-bootstrap-css', `${publicBaseUrl}/bootstrap/css/bootstrap.min.css`);
    ensureLink('legacy-bundle-css', `${publicBaseUrl}/assets/styles/bundle.css`);

    await ensureScript('legacy-bootstrap-js', `${publicBaseUrl}/bootstrap/js/bootstrap.bundle.min.js`);

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