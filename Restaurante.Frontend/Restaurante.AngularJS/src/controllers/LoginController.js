import UserServices from "@services/UserServices";

export default class LoginController {
    
    /**
     * @param {UserServices} UserServices,
     * @param {angular.ILocationService} $location,
    */
    constructor(UserServices, $location, RouteServices){
        this.UserServices = UserServices;
        this.location = $location;
        this.RouteServices = RouteServices;
        this.login = this.login.bind(this);
        this.email = '';
        this.password = '';
    }

    get ShowView(){
        return !this.UserServices.IsLogged;
    }

    login(){
        this.RouteServices.NextUrl = '/home';
        this.UserServices.login(this.email, this.password);
    }
}