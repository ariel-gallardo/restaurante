import angular from "angular";
import ngRoute from "angular-route";
import ngCookies from "angular-cookies";
import ngResource from "angular-resource";
import ngSanitize from "angular-sanitize";
import ngAria from "angular-aria";
import ngRedux from "ng-redux";
import { combineReducers } from "redux";
import Routes from "@routes";
import Components from "@components";
import Controllers from "@controllers";
import Services from "@services";
import Interceptors from "@interceptors";
import Filters from "@filters";
import ORDER_STATUS from "@models/Order/OrderStatus";
import { orderStatusReducer } from "@store/orderStatusStore";

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

// Register the RestauranteModule and configure its controllers, services, filters and components at the module level.
let RestauranteModule;
try {
    RestauranteModule = angular.module("RestauranteModule");
} catch (e) {
    RestauranteModule = angular.module("RestauranteModule", [
        ngRoute, ngCookies, ngResource, ngAria, ngSanitize, ngRedux
    ]);
}

// Global app constant available from any AngularJS controller/service.
RestauranteModule.constant('ORDER_STATUS', ORDER_STATUS);

RestauranteModule.config(['$ngReduxProvider', function ($ngReduxProvider) {
    const rootReducer = combineReducers({
        orderState: orderStatusReducer,
    });
    const enhancers = window.__REDUX_DEVTOOLS_EXTENSION__
        ? [window.__REDUX_DEVTOOLS_EXTENSION__({ name: 'v1-angularjs-redux' })]
        : [];
    $ngReduxProvider.createStoreWith(rootReducer, [], enhancers);
}]);

RestauranteModule.config(['$provide', function ($provide) {
    $provide.decorator('$ngRedux', ['$delegate', '$rootScope', function ($delegate, $rootScope) {
        const ngrxStore = window.angularStore;
        if (ngrxStore) {
            const mapNgrxStateToV1State = (ngrxState) => {
                const sharedShell = ngrxState?.sharedShell || {};
                return {
                    ...ngrxState,
                    orderState: {
                        ORDER_STATUS: sharedShell.ORDER_STATUS || 'SEARCHING'
                    }
                };
            };

            return {
                dispatch: (action) => {
                    ngrxStore.dispatch(action);
                    return action;
                },
                subscribe: (listener) => {
                    const subscription = ngrxStore.subscribe(() => {
                        $rootScope.$evalAsync(() => {
                            listener();
                        });
                    });
                    return () => subscription.unsubscribe();
                },
                getState: () => {
                    const ngrxState = window.angularStoreState || {};
                    return mapNgrxStateToV1State(ngrxState);
                },
                connect: (mapStateToThis, mapDispatchToThis) => {
                    return (target) => {
                        const updateTarget = () => {
                            const ngrxState = window.angularStoreState || {};
                            const v1State = mapNgrxStateToV1State(ngrxState);
                            const attrs = mapStateToThis(v1State);
                            Object.assign(target, attrs);
                        };

                        updateTarget();

                        const subscription = ngrxStore.subscribe(() => {
                            $rootScope.$evalAsync(() => {
                                updateTarget();
                            });
                        });
                        return () => subscription.unsubscribe();
                    };
                }
            };
        }

        console.warn('NgRx Store not found on window.angularStore. Shared state sync will be disabled.');
        return $delegate;
    }]);
}]);

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

export const mount = async (containerElement) => {
    const publicBaseUrl = getPublicBaseUrl();

    ensureLink('legacy-bootstrap-css', `${publicBaseUrl}/bootstrap/css/bootstrap.min.css`);
    ensureLink('legacy-bundle-css', `${publicBaseUrl}/assets/styles/bundle.css`);

    await ensureScript('legacy-bootstrap-js', `${publicBaseUrl}/bootstrap/js/bootstrap.bundle.min.js`);

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
        
        // Only bootstrap if the element is not already bootstrapped or has a scope
        const isAlreadyBootstrapped = el.classList.contains('ng-scope') || el.querySelector('.ng-scope') !== null;
        if (!isAlreadyBootstrapped) {
            angular.bootstrap(el, ["RestauranteModule"]);
        }
    } else {
        console.error("No se encontró el contenedor para montar RestauranteModule");
    }
};