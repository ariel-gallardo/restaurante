import UserServices from "@services/UserServices";

export default class LoginController {
    
    /**
     * @param {UserServices} UserServices,
     * @param {angular.ILocationService} $location,
     * @param {angular.IRootScopeService} $scope,
    */
    constructor(UserServices, $location, $scope){
        this.location = $location;
        this.login = this.login.bind(this);
        this.email = '';
        this.password = '';
        this.UserServices = UserServices;
    }

    get ShowView(){
        return !this.UserServices.IsLogged;
    }

    login(){
        this.UserServices.login(this.email, this.password);
    }
}