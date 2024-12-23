import RestauranteModule from "@modules/RestauranteModule";
import NavBarController from "@controllers/NavBarController";

export default RestauranteModule
.component("navBar", {
    templateUrl: '/components/views/NavBarComponent.html5',
    controller: NavBarController
});
