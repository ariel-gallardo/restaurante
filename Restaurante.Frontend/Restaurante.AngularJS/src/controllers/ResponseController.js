export default class ResponseController{

    constructor(ResponseServices, $rootScope, $timeout) {
        this.ResponseServices = ResponseServices;
        this.Toast = angular.element(document.querySelector('#responseToast'))[0];
        this.BootstrapToast = bootstrap.Toast.getOrCreateInstance(this.Toast);
        this.RootScope = $rootScope;
        this.timeout = $timeout;

        this.RootScope.$on('showResponseToast',() => {
            this.BootstrapToast.show();
            this.timeout(() => {
                this.BootstrapToast.hide();
            },this.timeoutTime);
        });
    }

    get statusCode(){
        return this.ResponseServices.statusCode;
    }

    get message(){
        return this.ResponseServices.message;
    }

    get messages(){
        return this.ResponseServices.messages;
    }

    get data(){
        return this.ResponseServices.data;
    }

    get show(){
        return this.ResponseServices.show;
    }

    get timeoutTime(){
        return this.ResponseServices.timeoutTime;
    }
}