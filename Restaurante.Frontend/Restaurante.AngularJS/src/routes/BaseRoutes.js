export default ($route) => {
    $route
        .when('/home', { 
            controller: "HomeCtrl",
            templateUrl: '/views/HomeView.html5',
            controllerAs: 'ctrl'
        })
        .when('/not-found', { 
            controller: "NotFoundCtrl",
            templateUrl: '/views/NotFoundView.html5',
            controllerAs: 'ctrl'
        })
        .otherwise({ redirectTo: '/home' });
}