import UserServices from "@services/UserServices";

export default class LoginController {
    
    /**
     * @param {UserServices} UserServices
    */
    constructor(UserServices, $location){
        this.location = $location;
        this.login = this.login.bind(this);
        this.email = '';
        this.password = '';
        this.UserServices = UserServices;
    }

    login(){
        this.UserServices.login(this.email, this.password);
    }
}