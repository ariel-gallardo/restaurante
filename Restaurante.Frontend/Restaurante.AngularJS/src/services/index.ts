import EnvironmentServices from "@services/EnvironmentServices";
import ApiServices from "./ApiServices";
import ResponseServices from "./ResponseServices";
import { RestauranteServices } from "./RestauranteServices";
import UserServices from "./UserServices";
import GeoRefServices from "@services/GeoRefServices";
import RouteServices from "@services/RouteServices";
import LocalStorageServices from "@services/LocalStorageServices";

export default [
    ["EnvironmentServices", EnvironmentServices],
    ["LocalStorageServices",LocalStorageServices],
    ["RouteServices",RouteServices],
    ["ApiServices", ApiServices],
    ["UserServices", UserServices],
    ["ResponseServices", ResponseServices],
    ["GeoRefServices", GeoRefServices],
    ["RestauranteServices", RestauranteServices],
]