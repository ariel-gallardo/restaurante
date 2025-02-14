import EnvironmentServices from "@services/EnvironmentServices";
import MessageServices from "@services/MessageServices";
import angular, { IPromise, IRootScopeService, ITimeoutService } from "angular";
import {Toast} from 'bootstrap';

export default class ResponseController{

    private ToastElement: Element;
    private BootstrapToast:any

    constructor(private $rootScope: IRootScopeService, private $timeout: ITimeoutService, private MessageServices: MessageServices, private EnvironmentServices: EnvironmentServices) {
        this.ToastElement = angular.element(document.querySelector('#responseToast'))[0];
        this.BootstrapToast = Toast.getOrCreateInstance(this.ToastElement,{autohide: true, delay: this.EnvironmentServices.ResponseTimeout, animation: true});
        this.$rootScope.$on('showResponseToast',() => {
            this.BootstrapToast.show();
        });
    }

    public HiddeToast(){
        this.BootstrapToast.hide();
    }

    public get statusCode(){
        return this.MessageServices.statusCode;
    }

    public get message(){
        return this.MessageServices.message;
    }

    public get messages(){
        return this.MessageServices.messages;
    }

    public get show(){
        return this.MessageServices.show;
    }

    public get timeoutTime(){
        return Number(this.MessageServices.timeoutTime);
    }
}