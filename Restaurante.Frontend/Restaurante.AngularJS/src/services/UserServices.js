export default class UserServices {

    constructor(ApiServices) {
        this.ApiServices = ApiServices;
        this.login = this.login.bind(this);
    }

    async login(email, password){
        return await this.ApiServices.post('/api/user/login', {email, password});
    }
}