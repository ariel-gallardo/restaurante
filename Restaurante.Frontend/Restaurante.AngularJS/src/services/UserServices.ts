import RegisterDTO from "@models/RegisterDTO";
import ApiServices from "@services/ApiServices";
import { RestauranteServices } from "@services/RestauranteServices";
import angular, { IRootScopeService } from "angular";
import { IFilterService, IIntervalService, ILocationService, IPromise, ITimeoutService } from "angular";

export default class UserServices {

    ApiServices : ApiServices;
    localStorage : Storage;
    cookies : angular.cookies.ICookiesService;
    location : ILocationService;
    interval : IIntervalService;
    filter : IFilterService;
    timeout : ITimeoutService;
    FirstInstance : boolean;
    RServices: RestauranteServices;
    ExpirationTimeFilter : any;
    ShowView: boolean;
    userInfo: any;
    TimeExpirationTokenDate: string;
    TimeExpirationTokenTime: string;
    TimeExpirationFirst: boolean;
    TimeoutTokenTime: IPromise<any>;

    constructor(ApiServices : ApiServices, $window : Window, $cookies : angular.cookies.ICookiesService, $location: ILocationService, $interval: IIntervalService, $filter: IFilterService, $timeout: ITimeoutService) {
        this.ApiServices = ApiServices;
        this.localStorage = $window.localStorage;
        this.cookies = $cookies;
        this.location = $location;
        this.interval = $interval;
        this.filter = $filter;
        this.timeout = $timeout;
        this.FirstInstance = true;
        this.ExpirationTimeFilter = this.filter('ExpirationTime');
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
        this.InitUserServices = this.InitUserServices.bind(this);
        this.DestroyUserServices = this.DestroyUserServices.bind(this);
        this.ShowView = false;
    }

    get IsLogged(){
        return this.ShowView;
    }

    InitUserServices(){
        this.userInfo = this.localStorage.userInfo ? JSON.parse(this.localStorage.userInfo) : {};
        this.TimeExpirationTokenDate = this.userInfo?.caducaEn ?? '-';
        this.TimeExpirationTokenTime = '-';
        if(this.FirstInstance){
            this.FirstInstance = false;
            this.TimeExpirationFirst = true;
        }
        this.TimeoutTokenTime = this.interval(() => this.UpdateExpirationTime(),1000);
    }

    DestroyUserServices(){
        try{
            this.interval.cancel(this.TimeoutTokenTime); 
        }catch(e){

        }
    }

    UpdateExpirationTime(){
        this.TimeExpirationTokenTime = this.ExpirationTimeFilter(this.TimeExpirationTokenDate);
        
        if(this.TimeExpirationFirst){
            this.TimeExpirationFirst = false;
             if(!this.cookies.get('auth_token') || this.TimeExpirationTokenTime == '-'){
                if(!this.IsRegisterPage || !this.IsLoginPage) this.RedirectToLogin;
             }
            else{
                this.ShowView = true;
            }
        }
        else if (this.TimeExpirationTokenTime == '-'){
                this.interval.cancel(this.TimeoutTokenTime);     
                this.ShowView = false;
                this.cookies.remove('auth_token');
                this.localStorage.userInfo = null; 
                let noRedirectSites = [
                    !this.IsLoginPage,
                    !this.IsRegisterPage
                ];
                if(noRedirectSites.every(x => x)) this.RedirectToLogin;
        }
        else if((this.IsLoginPage || this.IsRegisterPage) && this.TimeExpirationTokenTime != '-'){
            this.ShowView = true;
            this.RedirectToProfile;
        }
  }



    get RedirectToLogin(){
        this.location.path('/login');
        return true;
    }

    get RedirectToProfile(){
        this.location.path('/profile');
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
        return this.location.url();
    }

    logout(){
        try{
            this.cookies.remove('auth_token');
            this.localStorage.userInfo = null;
            this.interval.cancel(this.TimeoutTokenTime);    
        }catch(e){
        }finally{
            if(!this.location.url().includes('/login'))
                this.location.path('/login');
        }
    }
 
    login(email, password){
        this.ApiServices.post('/api/user/login', {correo: email, password});
    }

    register(data : RegisterDTO){
        this.ApiServices.post('/api/user/register',data);
    }

    get NombreCompleto(){
       return this.userInfo?.nombreCompleto ?? '-';
    }

    get Correo(){
        return this.userInfo?.correo ?? '-';
    }

    get Rol(){
        return this.userInfo?.tipoDeUsuario ?? '-';
    }

    get Domicilio(){
        return this.userInfo?.domicilio ?? '-';
    }

    get Telefono(){
        return this.userInfo?.telefono ?? '-';
    }

    get ImagenUrl(){
        return this.userInfo?.imagenUrl ?? '/assets/images/commonUser.svg';
    }

    get TiempoExpiracionToken(){
        return this.TimeExpirationTokenTime;
    }
}