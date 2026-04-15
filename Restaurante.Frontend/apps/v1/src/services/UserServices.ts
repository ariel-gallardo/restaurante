import RegisterDTO from "@models/User/RegisterDTO";
import UpdateUserDTO from "@models/User/UpdateUserDTO";
import ApiServices from "@services/ApiServices";
import LocalStorageServices from "@services/LocalStorageServices";
import angular, { IRootScopeService } from "angular";
import { ILocationService } from "angular";

export default class UserServices {

    static $inject = ['ApiServices', '$cookies', '$location', '$rootScope', 'LocalStorageServices'];
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
       return this.LocalStorageServices.CurrentUser.nombreCompleto;
    }

    get Correo(){
        return this.LocalStorageServices.CurrentUser.correo;
    }

    get Rol(){
        return this.LocalStorageServices.CurrentUser.tipoDeUsuario;
    }

    get Domicilio(){
        return this.LocalStorageServices.CurrentUser.domicilio;
    }

    get Telefono(){
        return this.LocalStorageServices.CurrentUser.telefono;
    }

    get ImagenUrl(){
        return this.LocalStorageServices.CurrentUser.imagenUrl;
    }

    get TiempoExpiracionToken(){
        return this.LocalStorageServices.CurrentUser.tiempoExpiracionToken;
    }

    public get Pedido(){
        return this.LocalStorageServices.CurrentUser.pedido;
    }

    public get DetallesPedido(){
        return this.Pedido.data;
    }
    

    public get Token()
    {
        return this.cookies.get('auth_token');
    }
}