import Order from "@models/Order/Order";
import OrderDetail from "@models/Order/OrderDetail";
import Response from "@models/Response";
import UserInfo from "@models/User/UserInfo";
import ApiServices from "@services/ApiServices";
import { cookies, IFilterService, IRootScopeService, IWindowService } from "angular";

export default class LocalStorageServices{

    static $inject = ['$rootScope','$filter','$window', '$cookies', 'ApiServices'];
    private LS : Storage;
    private ExpirationTimeFilter: (dateString) => '';
    private _u : UserInfo;

    public get CurrentUser(){
        if(!this._u) this._u = new UserInfo;
        this._u.tiempoExpiracionToken = this.ExpirationTimeFilter(this._u.caducaEn);
        this._u.imagenUrl = this._u.imagenUrl ?? '/assets/images/commonUser.svg';
        return this._u;
    }

    private async UserInfo(){
        let done = false;
        if((!this._u || this._u?.correo == '-') && this.$cookies.get('auth_token')){
            try{
                let res = await this.ApiServices.get('/api/user/info') as Response<UserInfo>;
                this._u = res.content;
                done = true;
                this.LS.setItem('userInfo',JSON.stringify(this._u));
                this.$rootScope.$emit('CheckOrder');
            }catch(e){
                this.RemoveUserInfo();
            }finally{
                if(!done && !this._u)
                {
                    this._u = new UserInfo;
                    this.LS.setItem('userInfo',JSON.stringify(this._u))
                }
            }
        }
        return this._u;
    };

    RemoveUserInfo(){
        this.LS.removeItem('userInfo');
    }

    constructor(private $rootScope: IRootScopeService, $filter: IFilterService, $window: IWindowService, private $cookies: cookies.ICookiesService, private ApiServices: ApiServices) {
        this.LS = $window.localStorage;
        if(!this.$cookies.get('auth_token')) this.RemoveUserInfo();
        this.ExpirationTimeFilter = $filter('ExpirationTime');
        let iUpdateInfo = setInterval( async () => {
            this._u = await this.UserInfo() ?? new UserInfo;
        },500);
    }
}