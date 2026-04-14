import HomeController from "./HomeController";
import LoginController from "./LoginController";
import NavBarController from "./NavBarController";
import RegisterController from "./RegisterController";
import { RestauranteController } from "./RestauranteController";
import UserController from "./UserController";
import NotFoundController from "./NotFoundController";
import MessageController from "./MessageController";
import CartController from "@controllers/CartController";
import OrderItemController from "@controllers/OrderItemController";
import OrderListController from "@controllers/OrderListController";

export default [
    ["RestauranteCtrl",RestauranteController],
    ["HomeCtrl",HomeController],
    ["NavBarCtrl",NavBarController],
    ["UserCtrl",UserController],
    ["RegisterCtrl",RegisterController],
    ["LoginCtrl",LoginController],
    ["NotFoundCtrl",NotFoundController],
    ["MessageCtrl",MessageController],
    ["OrderItemCtrl",OrderItemController],
    ["OrderListCtrl",OrderListController],
    ["CartCtrl",CartController]
]