import { IAttributes, IRootElementService, IRootScopeService, IScope } from "angular";

export default class OrdersListController{
    private _data = [];

    public get Data(){
        return this._data;
    }

    private _permissions = [];

    public get Permissions(){
        return this._permissions;
    }

    static $inject = ['$rootScope', '$scope', '$element', '$attrs'];
    
    constructor(private $rootScope : IRootScopeService, private $scope : IScope, private $element: IRootElementService, private $attrs : IAttributes) {
        
        $scope.$watchCollection('ctrl.data',async (nV : any[]) =>  {
            this._data = nV;
            await this.$scope.$applyAsync();
        });

        $scope.$watchCollection('ctrl.permissions', async (nV: any[]) => {
            this._permissions = nV;
            await this.$scope.$applyAsync();
        });
    }
}