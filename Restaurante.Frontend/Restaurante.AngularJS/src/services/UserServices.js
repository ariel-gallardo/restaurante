import ApiServices from "@services/ApiServices";
import angular from "angular";

export default class UserServices {

/**
 * @param {ApiServices} ApiServices,
 * @param {Window} $window,
 * @param {angular.cookies.ICookiesService} $cookies,
 * @param {angular.ILocationService} $location,
 * @param {angular.IIntervalService} $interval,
 * @param {angular.IFilterService} $filter,
 * @param {angular.ITimeoutService} $timeout,
*/
    constructor(ApiServices, $window, $cookies, $location, $interval, $filter, $timeout) {
        this.ApiServices = ApiServices;
        this.localStorage = $window.localStorage;
        this.cookies = $cookies;
        this.location = $location;
        this.interval = $interval;
        this.filter = $filter;
        this.timeout = $timeout;
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

    /**
     * @param {angular.IRootScopeService} $scope 
     */
    ConfigureUserServices($scope){
        $scope.$on('$viewContentLoaded',() => {
            this.InitUserServices();
        });
        $scope.$on('$destroy',() => {
            this.DestroyUserServices();
        });
    }

    InitUserServices(){
        this.userInfo = this.localStorage.userInfo ? JSON.parse(this.localStorage.userInfo) : {};
        this.TimeExpirationTokenDate = this.userInfo?.caducaEn ?? '-';
        this.TimeExpirationTokenTime = '-';
        this.TimeExpirationFirst = true;
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
                if(!this.IsLoginPage) this.RedirectToLogin;
             }
            else{
                this.ShowView = true;
            }
        }
         if (this.TimeExpirationTokenTime == '-'){
            try{
                this.cookies.remove('auth_token');
                this.localStorage.userInfo = null;
                this.interval.cancel(this.TimeoutTokenTime);     
            }catch(e){
            }finally{
                this.ShowView = false;
                if(!this.IsLoginPage) this.RedirectToLogin;
            }
        }
        if((this.IsLoginPage || this.IsRegisterPage) && this.TimeExpirationTokenTime != '-'){
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

    async login(email, password){
        return await this.ApiServices.post('/api/user/login', {correo: email, password});
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