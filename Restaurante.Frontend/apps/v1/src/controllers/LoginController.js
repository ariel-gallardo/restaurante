import UserServices from "@services/UserServices";
import { TRANSLATIONS } from "@org/shared-shell";

export default class LoginController {
    static $inject = ['UserServices', '$location', 'RouteServices', '$ngRedux'];
    
    /**
     * @param {UserServices} UserServices
     * @param {angular.ILocationService} $location
     * @param {any} RouteServices
     * @param {any} $ngRedux
     */
    constructor(UserServices, $location, RouteServices, $ngRedux){
        this.UserServices = UserServices;
        this.location = $location;
        this.RouteServices = RouteServices;
        this.$ngRedux = $ngRedux;
        this.login = this.login.bind(this);
        this.email = '';
        this.password = '';
        this.currentLanguage = 'es';

        const mapStateToThis = (state) => ({
            currentLanguage: state?.orderState?.language || 'es'
        });
        this.unsubscribeRedux = this.$ngRedux.connect(mapStateToThis)(this);
    }

    get t() {
        const lang = this.currentLanguage || 'es';
        return TRANSLATIONS[lang] || TRANSLATIONS.es;
    }

    get ShowView(){
        return !this.UserServices.IsLogged;
    }

    login(){
        this.RouteServices.NextUrl = '/home';
        this.UserServices.login(this.email, this.password);
    }

    $onDestroy() {
        if (this.unsubscribeRedux) {
            this.unsubscribeRedux();
        }
    }
}