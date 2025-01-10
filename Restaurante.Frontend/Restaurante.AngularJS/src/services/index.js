import EnvironmentServices from "@services/EnvironmentServices";
import ApiServices from "./ApiServices";
import ResponseServices from "./ResponseServices";
import { RestauranteServices } from "./RestauranteServices";
import UserServices from "./UserServices";
import GeoRefServices from "@services/GeoRefServices";

export default [
    ["EnvironmentServices", EnvironmentServices],
    ["ApiServices", ApiServices],
    ["ResponseServices", ResponseServices],
    ["GeoRefServices", GeoRefServices],
    ["UserServices", UserServices],
    ["RestauranteServices", RestauranteServices],
]