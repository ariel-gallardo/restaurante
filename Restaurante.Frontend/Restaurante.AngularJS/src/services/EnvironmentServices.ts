export default class EnvironmentServices{

    public get Provincia(){
        return process.env.PROVINCIA;
    }
    public get ResponseTimeout(){
        return parseInt(process.env.RESPONSE_TIMEOUT);
    }
    public get ApiAdress(){
        return process.env.API_ADDRESS;
    }
    public get WsAddress(){
        return process.env.WS_ADDRESS;
    }

    public get WsAddressPedido(){
        return `${this.WsAddress}/pedidos`;
    }

    public get WsAddressMessages(){
        return `${this.WsAddress}/messages`;
    }

    public get WsAddressDelivery(){
        return `${this.WsAddress}/delivery`;
    }

    public get WsRetryMS() : number{
        return Number(process.env.WS_RETRY_MS);
    }

    public get WsRetryTimes():number{
        return Number(process.env.WS_RETRY_TIMES);
    }

    public get MapsApiKey(){
        return process.env.MAPS_API_KEY;
    }

    public get RestaurantePosition(){
        return [-32.90775874265699, -68.80538460717636];
    }
}