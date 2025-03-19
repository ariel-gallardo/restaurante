/**
 * @param {angular.route.IRouteProvider} $route,
*/
export default ($route) => {
    $route
        .when('/work', { 
            controller: "WorkCtrl",
            templateUrl: '/views/WorkView.html5',
            controllerAs: 'ctrl'
        })
        .otherwise({ redirectTo: '/home' });
}