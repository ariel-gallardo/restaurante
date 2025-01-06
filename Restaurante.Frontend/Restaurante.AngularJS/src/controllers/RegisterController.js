export default class RegisterController{

    /**
     * @param {UserServices} UserServices,
     * @param {angular.ILocationService} $location,
     * @param {angular.IRootScopeService} $scope,
    */
    constructor(UserServices, $location, $scope){
        this.location = $location;
        this.name = '';
        this.surname = '';
        this.email = '';
        this.password = '';
        this.re_password = '';
        this.codearea = '';
        this.phone = '';
        this.UserServices = UserServices;
        this.UserServices.ConfigureUserServices($scope);
        this.register = this.register.bind(this); 
    }

    get ShowView(){
        return !this.UserServices.IsLogged;
    }

    register(){

    }
    
}