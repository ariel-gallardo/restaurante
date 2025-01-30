import ResponseServices from "@services/ResponseServices";

export default class ResponseController{

/**
 * @param {ResponseServices} ResponseServices
 * @param {angular.IRootScopeService} $rootScope 
 * @param {angular.ITimeoutService} $timeout 
 */
    constructor(ResponseServices, $rootScope, $timeout) {
        this.ResponseServices = ResponseServices;
        this.Toast = angular.element(document.querySelector('#responseToast'))[0];
        this.BootstrapToast = bootstrap.Toast.getOrCreateInstance(this.Toast);
        this.RootScope = $rootScope;
        this.timeout = $timeout;
        this.currentTimeout = null;
        
        this.RootScope.$on('showResponseToast',() => {
            this.BootstrapToast.show();
            this.currentTimeout = this.timeout(() => {
                this.BootstrapToast.hide();
            },this.timeoutTime);
        });
    }

    get HiddeToast(){
        if(this.currentTimeout != null)
        {
            this.timeout.cancel(this.currentTimeout);
            this.currentTimeout = null;
            this.BootstrapToast.hide();
        }
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