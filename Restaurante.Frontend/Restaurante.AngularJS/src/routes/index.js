import BaseRoutes from "./BaseRoutes";
import CartRoutes from "./CartRoutes";
import UserRoutes from "./userRoutes";

export default ($routeProvider, $locationProvider) => {
    $locationProvider.html5Mode(true);
    BaseRoutes($routeProvider);
    UserRoutes($routeProvider);
    CartRoutes($routeProvider);
}

//export default RestauranteModule;