import UserServices from "@services/UserServices";
import angular, { ILocationService, IScope } from "angular";
import { IRootScopeService } from "angular";

export class RestauranteServices {

    private _nextUrl : string = null;
    private RootScope: IRootScopeService;
    private Location: ILocationService;
    private UserServices: UserServices;
    private $: JQueryStatic;
    private CurrentAuthScope: IScope;
    
    constructor($rootScope: IRootScopeService, $location : ILocationService, UserServices: UserServices, $window: any) {
        this.Location = $location;
        this._nextUrl = null;    
        this.RootScope = $rootScope;
        this.UserServices = UserServices;
        this.UserServices.InitUserServices();
        this.RootScope.$on('$destroy',() => {
            this.UserServices.DestroyUserServices();
        });
        this.$ = $window.$; 
        this.RootScope.$on('$viewContentLoaded',() => this.AuthMiddleWare());
    }

    AuthMiddleWare(){
        let currentUrl = this.Location.url();
        let authViewData = angular.element(this.$('[Auth]'));
        if(authViewData.length > 0){
            this.CurrentAuthScope = authViewData.scope();
            if(!this.UserServices.IsLogged)
                this.Location.path('/login');
            else if(this.UserServices.IsLogged && (currentUrl == '/register' || currentUrl == '/login'))
                this.Location.path('/home');
        }
        else
            this.CurrentAuthScope = null;
    }
    
    public get NextUrl(){
        let nUrl = this._nextUrl;
        this._nextUrl = '';
        return nUrl;
    }

    public set NextUrl(nUrl){
        if(nUrl && nUrl != ''){
            this._nextUrl = nUrl;
        }
    }

}