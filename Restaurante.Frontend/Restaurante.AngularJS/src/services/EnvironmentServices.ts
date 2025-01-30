export default class EnvironmentServices{
    get Provincia(){
        return process.env.PROVINCIA;
    }
    get ResponseTimeout(){
        return parseInt(process.env.RESPONSE_TIMEOUT);
    }
    get ApiAdress(){
        return process.env.API_ADDRESS;
    }
}