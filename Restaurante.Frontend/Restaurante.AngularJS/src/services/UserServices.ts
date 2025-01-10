import RegisterDTO from "@models/User/RegisterDTO";
import UpdateUserDTO from "@models/User/UpdateUserDTO";
import ApiServices from "@services/ApiServices";
import LocalStorageServices from "@services/LocalStorageServices";
import angular, { IRootScopeService } from "angular";
import { ILocationService } from "angular";

export default class UserServices {

    private ApiServices : ApiServices;
    private cookies : angular.cookies.ICookiesService;
    private location : ILocationService;
    private LocalStorageServices: LocalStorageServices;
    private TiempoExpiracion: string;
    private RootScope: IRootScopeService;
    
    constructor(ApiServices : ApiServices, $cookies : angular.cookies.ICookiesService, $location: ILocationService, $rootScope: IRootScopeService, LocalStorageServices: LocalStorageServices) {
        this.ApiServices = ApiServices;
        this.cookies = $cookies;
        this.location = $location;
        this.LocalStorageServices = LocalStorageServices;
        this.RootScope = $rootScope;
        this.RootScope.$on('UpdateTokenTime', (e,time) => {this.TiempoExpiracion = time;});
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
    }

    logout(){
        try{
            this.cookies.remove('auth_token');
            this.LocalStorageServices.RemoveUserInfo();
            this.RootScope.$emit('CancelTokenTime'); 
        }catch(e){
        }finally{
            if(!this.location.url().includes('/login'))
                this.location.path('/login');
        }
    }

    update(data: UpdateUserDTO){
        this.ApiServices.put('/api/user',data);
    }
 
    login(email, password){
        this.ApiServices.post('/api/user/login', {correo: email, password});
    }

    register(data : RegisterDTO){
        this.ApiServices.post('/api/user/register',data);
    }

    get NombreCompleto(){
       return this.LocalStorageServices.UserInfo.NombreCompleto;
    }

    get Correo(){
        return this.LocalStorageServices.UserInfo.Correo;
    }

    get Rol(){
        return this.LocalStorageServices.UserInfo.Rol;
    }

    get Domicilio(){
        return this.LocalStorageServices.UserInfo.Domicilio;
    }

    get Telefono(){
        return this.LocalStorageServices.UserInfo.Telefono;
    }

    get ImagenUrl(){
        return this.LocalStorageServices.UserInfo.ImagenUrl;
    }

    get TiempoExpiracionToken(){
        return this.TiempoExpiracion;
    }
}