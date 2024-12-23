import HomeController from "@controllers/HomeController";
import RestauranteModule from "@modules/RestauranteModule";
//import HomeView from "@views/HomeView.html5";

export default RestauranteModule.config(($routeProvider, $locationProvider) => {
    $locationProvider.html5Mode(true);
    $routeProvider
    .when('/home', { 
        controller: HomeController,
        templateUrl: '/views/HomeView.html5'
        //template: HomeView
    })
    .when('/cart', {templateUrl: '/views/NotFound.html5'})
    .when('/profile', {templateUrl: '/views/NotFound.html5'})
    .otherwise({ redirectTo: '/home' });
});

//export default RestauranteModule;