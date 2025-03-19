import Order from "@models/Order/Order";
import { IAttributes, IRootElementService, IRootScopeService, IScope } from "angular";

export default class OrdersItemController{

    static $inject = ['$rootScope', '$scope', '$element', '$attrs'];
    
    private _permissions = [];

    public get Permissions(){
        return this._permissions;
    }
    public order:Order = null;

    constructor(private $rootScope : IRootScopeService, public $scope : IScope, private $element: IRootElementService, private $attrs : IAttributes) {
        $scope.$watchCollection('ctrl.permissions', async (nV: any[]) => {
            this._permissions = nV;
            await this.$scope.$applyAsync();
        });
    }

}