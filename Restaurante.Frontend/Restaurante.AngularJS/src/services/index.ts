import EnvironmentServices from "@services/EnvironmentServices";
import ApiServices from "./ApiServices";
import ResponseServices from "./ResponseServices";
import { RestauranteServices } from "./RestauranteServices";
import UserServices from "./UserServices";
import GeoRefServices from "@services/GeoRefServices";
import RouteServices from "@services/RouteServices";
import LocalStorageServices from "@services/LocalStorageServices";
import ProductoServices from "@services/ProductoServices";
import CategoryServices from "@services/CategoryServices";

export default [
    ["EnvironmentServices", EnvironmentServices],
    ["LocalStorageServices",LocalStorageServices],
    ["RouteServices",RouteServices],
    ["ApiServices", ApiServices],
    ["CategoryServices", CategoryServices],
    ["ProductoServices", ProductoServices],
    ["UserServices", UserServices],
    ["ResponseServices", ResponseServices],
    ["GeoRefServices", GeoRefServices],
    ["RestauranteServices", RestauranteServices],
]