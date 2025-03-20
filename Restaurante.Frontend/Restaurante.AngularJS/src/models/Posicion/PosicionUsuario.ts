export default class PosicionUsuario{
    private _lat:number;
    private _lng:number;

    public get Latitud(){return this._lat};
    public get Longitud(){return this._lng};

    constructor(pos: google.maps.LatLng) {
        this._lat = pos.lat();
        this._lng = pos.lng();
    }
}