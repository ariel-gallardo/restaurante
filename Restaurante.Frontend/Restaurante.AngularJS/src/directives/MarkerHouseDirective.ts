import MarkerHouseController from "@controllers/directives/MarkerHouseController";
import MarkerHouseLink from "@links/MarkerHouseLink";

export default function MarkerHouseDirective() : angular.IDirective{
    return {
        require: '^mapGoogle',
        multiElement: true, 
        restrict: 'A',
        controller: MarkerHouseController,
        controllerAs: 'ctrl',
        transclude: true,
        bindToController: true,
        link: MarkerHouseLink
    } as angular.IDirective
}