import EnvironmentServices from "@services/EnvironmentServices";
import ApiServices from "./ApiServices";
import { RestauranteServices } from "./RestauranteServices";
import UserServices from "./UserServices";
import GeoRefServices from "@services/GeoRefServices";
import RouteServices from "@services/RouteServices";
import LocalStorageServices from "@services/LocalStorageServices";
import ProductoServices from "@services/ProductoServices";
import CategoryServices from "@services/CategoryServices";
import PedidosServices from "@services/PedidosServices";
import MessageServices from "@services/MessageServices";

export default [
    ["EnvironmentServices", EnvironmentServices],
    ["LocalStorageServices",LocalStorageServices],
    ["RouteServices",RouteServices],
    ["ApiServices", ApiServices],
    ["MessageServices",MessageServices],
    ["CategoryServices", CategoryServices],
    ["ProductoServices", ProductoServices],
    ["UserServices", UserServices],
    ["GeoRefServices", GeoRefServices],
    ["PedidosServices",PedidosServices],
    ["RestauranteServices", RestauranteServices],
]