export default class UserServices {

    constructor(ApiServices, $window, $cookies, $location, $interval, $filter) {
        this.ApiServices = ApiServices;
        this.localStorage = $window.localStorage;
        this.cookies = $cookies;
        this.location = $location;
        this.ExpirationTimeFilter = $filter('ExpirationTime');
        this.userInfo = this.localStorage.userInfo ? JSON.parse(this.localStorage.userInfo) : {};
        this.TimeExpirationTokenDate = this.userInfo?.caducaEn ?? '-';
        this.TimeExpirationTokenTime = '-';
        this.TimeExpirationFirst = true;
        this.TimeoutTokenTime = $interval(() => this.UpdateExpirationTime(),1000);
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
    }

    UpdateExpirationTime(){
        this.TimeExpirationTokenTime = this.ExpirationTimeFilter(this.TimeExpirationTokenDate);
        if(this.TimeExpirationFirst){
            this.TimeExpirationFirst = false;
             if(!this.cookies.get('auth_token') || this.TimeExpirationTokenTime == '-') this.location.path('/login');
        }else if (this.TimeExpirationTokenTime == '-'){
            try{
                this.cookies.remove('auth_token');
                this.localStorage.userInfo = null;
                this.location.path('/login');
            }catch(e){

            }
             
        }
  }

    logout(){
        try{
            this.cookies.remove('auth_token');
            this.localStorage.userInfo = null;
            this.location.path('/login');
        }catch(e){

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

    get TiempoExpiracionToken(){
        return this.TimeExpirationTokenTime;
    }
}