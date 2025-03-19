import { IAttributes, IRootElementService, IRootScopeService, IScope } from "angular";

export default class OrderItemController
{
    static $inject = ['$rootScope', '$scope', '$element', '$attrs'];

    public infoId:string = '-';
    public infoQuantity:number = 0;

    constructor(private $rootScope : IRootScopeService, public $scope : IScope, private $element: IRootElementService, private $attrs : IAttributes) {
    }
}