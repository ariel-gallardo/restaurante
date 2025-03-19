export default class PosicionDTO{
    private _latitude: number;
    private _longitude: number;
    private _heading: number;
    private _speed: number;
    private _time: string;

    private _pedidoId: string;
    private _deliveryId: number;

    public get DeliveryId(){return this._deliveryId;}
    public get PedidoId(){return this._pedidoId;}

    public get Latitud(){return this._latitude;}
    public get Longitud(){return this._longitude;}
    public get Direccion(){return this._heading;}
    public get Velocidad(){return this._speed;}
    public get Fecha(){return this._time;}

    public set DeliveryId(id: number){this._deliveryId = id;}
    public set PedidoId(id: string){this._pedidoId = id;}

    public set Latitud(lat: number){this._latitude = lat;}
    public set Longitud(lng: number){this._longitude = lng;}
    public set Direccion(d: number){this._heading = d;}
    public set Velocidad(v: number){this._speed = v;}
    public set Fecha(f: string){this._time = f;}

    public static FromGeoLocation(p: GeolocationPosition, orderId: string, deliveryId: string){
        let pos = new PosicionDTO;
        pos.Latitud = p.coords.latitude, 
        pos.Longitud = p.coords.longitude;
        pos.Direccion = p.coords.heading;
        pos.Velocidad = p.coords.speed;
        pos.Fecha = new Date(p.timestamp).toISOString();
        pos.DeliveryId = Number(deliveryId);
        pos.PedidoId = orderId;
    }

}