import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import EnvironmentServices from "@services/EnvironmentServices";
import { IRootScopeService } from "angular";

export default class MessageServices{
    static $inject = ['$rootScope', 'EnvironmentServices', '$location'];
    private _messagesHub: HubConnection;
    private _connectionId: string;

    public get ConnectionId(){return this._connectionId;}

    public statusCode: number;
    public message: string;
    public messages: string[];
    public show: boolean;
    public timeoutTime: string;    

    constructor(private $rootScope: IRootScopeService, 
        private EnvironmentServices: EnvironmentServices) {
        this.MessagesHub();
    }

    private MessagesHub(){
        if(this._messagesHub == null){
            this._messagesHub = new HubConnectionBuilder()
            .withUrl(this.EnvironmentServices.WsAddressMessages)
            .configureLogging(LogLevel.None)
            .withAutomaticReconnect()
            .build();
            this._messagesHub.on('ConnectionId',(id:string) => {
                this._connectionId = id;
                this.$rootScope.$emit('ConnectionId', this.ConnectionId);
                this._messagesHub.invoke('JoinChannel',this._connectionId);
            });
            this._messagesHub.onreconnecting(e => {
                this._connectionId = null;
                this.$rootScope.$emit('ConnectionId', this.ConnectionId);
            });
            this._messagesHub.onreconnected((id:string) => {
                this._connectionId = id;
                this.$rootScope.$emit('ConnectionId', this.ConnectionId);
                this._messagesHub.invoke('JoinChannel',this._connectionId);
            })
            this._messagesHub.onclose(e => {
                this._connectionId = null;
                this.$rootScope.$emit('ConnectionId', this.ConnectionId);
            });
            this._messagesHub.on('Message',(data:any) => {
                const {message, statusCode} = data;
                this.setMessageInfo(message,statusCode);
            });
            this._messagesHub.on('Operation',(m) => console.log(m));
            this._messagesHub.serverTimeoutInMilliseconds = 60000;
            this._messagesHub.start()
            .catch(e => `API Server not working`);
            
        }
    }

    private setMessageInfo(message, statusCode){
        let sI = message.indexOf(`"`);
        this.message = message.substring(0,sI);
        this.messages = message.replace(`"`,'').split('|');
        if(this.messages)
            this.messages[0] = this.messages[0].replace(this.message,'');
        this.statusCode = statusCode;
        this.show = true;
        this.$rootScope.$emit('showResponseToast');
    }
}