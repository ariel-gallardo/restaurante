/**
 * @param {angular.route.IRouteProvider} $route,
*/
export default ($route) => {
    $route.when('/cart', {
        templateUrl: '/views/CartView.html5',
        controller: 'CartCtrl',
        controllerAs: 'ctrl'
    })
}