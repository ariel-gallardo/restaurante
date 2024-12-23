import HomeController from "./HomeController";
import LoginController from "./LoginController";
import NavBarController from "./NavBarController";
import RegisterController from "./RegisterController";
import { RestauranteController } from "./RestauranteController";
import UserController from "./UserController";

export default [
    ["RestauranteCtrl",RestauranteController],
    ["HomeCtrl",HomeController],
    ["NavBarCtrl",NavBarController],
    ["UserCtrl",UserController],
    ["RegisterCtrl",RegisterController],
    ["LoginCtrl",LoginController],
]