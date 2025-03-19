import { IAttributes, IRootElementService, IRootScopeService, IScope } from "angular";

export default class OrderListController
{
    private _data = [];

    public get Data(){
        return this._data;
    }
    
    static $inject = ['$rootScope', '$scope', '$element', '$attrs'];
    constructor(private $rootScope : IRootScopeService, private $scope : IScope, private $element: IRootElementService, private $attrs : IAttributes) {
        
        $scope.$watchCollection('ctrl.data',async (nV : any[]) =>  {
            this._data = nV;
            await this.$scope.$applyAsync();
        } )
    }
}