import { RestauranteController } from "./RestauranteController";
import HomeController from "@controllers/views/HomeController";
import LoginController from "@controllers/views/LoginController";
import NavBarController from "@controllers/components/navbar/NavBarController";
import RegisterController from "@controllers/views/RegisterController";
import UserController from "@controllers/views/UserController";
import NotFoundController from "@controllers/views/NotFoundController";
import MessageController from "@controllers/components/message/MessageController";
import CartController from "@controllers/views/CartController";
import OrderItemController from "@controllers/components/order/OrderItemController";
import OrderListController from "@controllers/components/order/OrderListController";
import OrdersListController from "@controllers/components/orders/OrdersListController";
import OrdersItemController from "@controllers/components/orders/OrdersItemController";
import WorkController from "@controllers/views/WorkController";
import MapGoogleController from "@controllers/components/maps/MapGoogleController";
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
    ["OrdersListCtrl",OrdersListController],
    ["OrdersItemCtrl",OrdersItemController],
    ["CartCtrl",CartController],
    ["MapGoogleCtrl",MapGoogleController],
    ["WorkCtrl",WorkController]
]