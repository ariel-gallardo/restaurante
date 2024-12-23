export default ($route) => {
    $route
    .when('/profile', 
        {
            templateUrl: '/views/ProfileView.html5',
            controller: 'UserCtrl',
            controllerAs: 'ctrl'
        }
    ).when('/login', 
        {
            templateUrl: '/views/LoginView.html5',
            controller: 'LoginCtrl',
            controllerAs: 'ctrl'
        }
    ).when('/register', 
        {
            templateUrl: '/views/RegisterView.html5',
            controller: 'RegisterCtrl',
            controllerAs: 'ctrl'
        }
    ).when('/forbidden',{
        templateUrl: '/views/ForbiddenView.html5',
        controller: 'ForbiddenCtrl',
        controllerAs: 'ctrl'
    })
}