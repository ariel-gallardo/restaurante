import ICustomScope from "@models/IScopeController";
import LocalStorageServices from "@services/LocalStorageServices";
import angular, { IIntervalService, ILocationService, IRootScopeService, IPromise } from "angular";

export default class RouteServices {
    private static $inject = ['$rootScope','$location','$interval','$cookies', 'LocalStorageServices'];
    private _nUrl: string;
    private _authScope: ICustomScope;
    private _isAuthScope: boolean;

    public get IsLogged(){
        return this.LocalStorageServices?.CurrentUser?.tiempoExpiracionToken != '-';
    }

    public IsAuthRoute:boolean;

    public get NextUrl(){
        let nUrl = this._nUrl;
        this._nUrl = null;
        return nUrl;
    }

    public set NextUrl(nUrl){
        if(nUrl && nUrl != '')
            this._nUrl = nUrl;
    }

    private Location: ILocationService;
    private FirstInit: boolean;
    private TimeoutTokenTimePromise: IPromise<any>;
    private CheckAuthViewPromise: IPromise<any>;

     

    constructor(
        private $rootScope: IRootScopeService,
        private $location: ILocationService, 
        private $interval: IIntervalService,
        private $cookies: angular.cookies.ICookiesService,
        private LocalStorageServices: LocalStorageServices
        ) {
        this.FirstInit = true;
        this._authScope = null;
        this.$rootScope.$on('CheckNextUrl',() => {if(this._nUrl) this.$location.url(this.NextUrl);});
        this.$rootScope.$on('RemoveNextUrl',() => {this._nUrl = null;});
        this.$rootScope.$on('$destroy',() => this.RemoveCheckAuth());
        this.$rootScope.$on('CancelTokenTime', () => {if(this.TimeoutTokenTimePromise) this.$interval.cancel(this.TimeoutTokenTimePromise);})
        this.TimeoutTokenTimePromise = this.$interval(() => this.UpdateExpirationTime(),500);
        this.CheckAuthViewPromise = this.$interval(() => this.CheckAuthView(),500);
    }

    private CheckAuthView(){
        let authViewData = angular.element($('[Auth]'));
        let currentUrl = this.$location.url();

        if(authViewData.length > 0){
            this._authScope = authViewData.scope();
            this._isAuthScope = true;
            if(this.IsLogged && (currentUrl == '/register' || currentUrl == '/login'))
                this.$location.url('/profile');
            else if(!this.IsLogged && this._isAuthScope)
                this.$location.url('/login');
        }
        else
        {
            if(this.IsLogged && (currentUrl == '/register' || currentUrl == '/login'))
                this.$location.url('/profile');
            this._authScope = null;
            this._isAuthScope = false;
        }
    }

    private RemoveCheckAuth(){
        if(this.TimeoutTokenTimePromise) this.$interval.cancel(this.TimeoutTokenTimePromise); 
    }

    private UpdateExpirationTime(){
        let tE = this.LocalStorageServices.CurrentUser.tiempoExpiracionToken;
        this.$rootScope.$emit('UpdateTokenTime',tE);

        if(this.FirstInit){
            this.FirstInit = false;
             if(!this.$cookies.get('auth_token') || tE == '-'){
             }
        }
        else if (tE == '-'){
                this.$interval.cancel(this.TimeoutTokenTimePromise);     
                this.$cookies.remove('auth_token');
                this.LocalStorageServices.RemoveUserInfo();
        }
    }

    get RedirectToLogin(){
        this.$location.path('/login');
        return true;
    }

    get RedirectToProfile(){
        this.$location.path('/profile');
        return true;
    }

    get IsRegisterPage(){
        return this.CurrentUrl == '/register';
    }

    get IsLoginPage(){
        return this.CurrentUrl == '/login';
    }

    get IsProfilePage(){
        return this.CurrentUrl == '/profile';
    }

    get CurrentUrl(){
        return this.$location.url();
    }
}
