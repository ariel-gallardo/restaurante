import MapGoogleController from "@controllers/components/maps/MapGoogleController";
import { IAttributes, IRootElementService, IScope } from "angular";

export default function MarkerHouseLink ($scope : IScope, $element: IRootElementService, $attrs : IAttributes, $controller : MapGoogleController){
    $scope.$watchGroup([() => $controller.Map, () => $controller.OrderId], (nV) => {
        const [map, orderId] = nV;
            $scope['ctrl'].map = map;
            $scope['ctrl'].orderId = orderId;
            $scope['ctrl'].parentCtrl = $controller;
            $controller.AddDirectiveScope('MarkerHouse',$scope);
    });
}