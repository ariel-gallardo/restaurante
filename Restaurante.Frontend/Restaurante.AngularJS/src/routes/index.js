import WorkRoutes from "@routes/WorkRoutes";
import BaseRoutes from "./BaseRoutes";
import CartRoutes from "./CartRoutes";
import UserRoutes from "./UserRoutes";

export default ($routeProvider, $locationProvider) => {
    $locationProvider.html5Mode(true);
    BaseRoutes($routeProvider);
    UserRoutes($routeProvider);
    CartRoutes($routeProvider);
    WorkRoutes($routeProvider);
}

//export default RestauranteModule;