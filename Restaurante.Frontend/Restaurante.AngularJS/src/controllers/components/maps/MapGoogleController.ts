import PosicionDTO from "@models/Posicion/PosicionDTO";
import EnvironmentServices from "@services/EnvironmentServices";
import ClassMap from "@css/ClassMap";
import { IAngularEvent, IAttributes, IRootElementService, IRootScopeService, IScope } from "angular";
import MapButtonAddEventData from "@events/MapButtonAddEventData";

export default class MapGoogleController{
    static $inject = ['$rootScope','$scope','$element','$attrs','EnvironmentServices'];
    private _map: google.maps.Map;
    private _mapHtml: HTMLElement;
    private _timeout: NodeJS.Timeout;
    private _simulate: google.maps.LatLng[];
    private _deliveryId: number;
    private _orderId: string;
    private _vehicle: google.maps.Marker;
    private _mapClass: ClassMap = new ClassMap;
    private _latitude: number = 0.0;
    private _longitude: number = 0.0;
    private _width: string;
    private _height: string;
    private _mapCtrl: JQuery<HTMLSpanElement>;
    private _directiveOutputs = {};
    private _directiveScopes = {};

    //Directives
    public AddDirectiveOutput<T>(from: string, data:T){
        this._directiveOutputs = {...this._directiveOutputs, [from]: data};
    };

    public get DirectiveOutputs(){
        return this._directiveOutputs;
    }

    private SendDirectiveOutputs(){
        this.$scope.$emit('Directive_Output',this.DirectiveOutputs);
    }

    //Scopes
    public get DirectiveScopes(){
        return this._directiveScopes;
    }

    public AddDirectiveScope(from: string, data:IScope){
        this._directiveScopes = {...this._directiveScopes, [from]: data};
        this.$scope.$emit('Scopes_Output',this._directiveScopes);
    };

    public get Map(){
        return this._map;
    }

    public get OrderId(){
        return this._orderId;
    }

    constructor(
        private $rootScope : IRootScopeService, 
        private $scope : IScope, 
        private $element: IRootElementService, 
        private $attrs : IAttributes,
        private EnvironmentServices: EnvironmentServices
    ) {
        this.$scope.$watch(() => this.DirectiveOutputs, this.SendDirectiveOutputs.bind(this));

        this.$scope.$watchGroup(['ctrl.mapHeight', 'ctrl.mapWidth'],(nV:string) => {
            if(nV){
                const [h,w] = nV;
                if(h && w){
                    this._height = h;
                    this._width = w;
                }
            }
        });
        this.$scope.$watchGroup(['ctrl.mapLatitude', 'ctrl.mapLongitude'],(nV,oV) => {
            const [lat,lng] = nV;
            this._latitude = lat;
            this._longitude = lng;
        });
        this.$scope.$watch('ctrl.mapClass', (nV: ClassMap) => this._mapClass = nV);

        this.$scope.$on('Map_Create',this.createMap.bind(this));
        this.$scope.$on('Map_AddRoute',this.addRouteToMap.bind(this));
        this.$scope.$on('Map_Assign',this.assignDelivery.bind(this));
        this.$scope.$on('Map_Move',this.moveDelivery.bind(this));
        this.$scope.$on('Map_End', this.removeCurrent.bind(this));
        this.$scope.$on('Map_Button_Add', this.addButton.bind(this));
        this.$scope.$on('Map_Button_Remove', this.removeButton.bind(this));
        this.$rootScope.$on('Map_Move',(e: IAngularEvent, pos: PosicionDTO) => {
            if(pos.PedidoId == this._orderId)
                this.$scope.$emit('Map_Move',pos);
        });
    }

    private generateRotatedIcon(imageUrl: string, angle: number, size: number = 40): string {
        const svg = `
            <svg xmlns="http://www.w3.org/2000/svg" width="${size}" height="${size}" viewBox="0 0 ${size} ${size}">
                <g transform="rotate(${angle}, ${size/2}, ${size/2})">
                    <image href="${imageUrl}" x="0" y="0" width="${size}" height="${size}" />
                </g>
            </svg>
        `;
        const svgBase64 = btoa(svg);
        return `data:image/svg+xml;base64,${svgBase64}`;
    }
    

    private simulateDelivery(e: IAngularEvent) {
        let index = 0;
        const delay = 300;
        let timeoutId: number | undefined;
        let path: google.maps.LatLng[] = this._simulate;
        if(path){
            const mover = () => {
                if (index < path.length) {
                    const point = path[index];
                    let pos = new PosicionDTO;
                    pos.Latitud = point.lat();
                    pos.Longitud = point.lng();
                    pos.PedidoId = 'Simulate';
                    this.moveDelivery(e,pos);
                    index++;
                    this._timeout = setTimeout(mover, delay);
                } else {
                    this.removeCurrent(e);
                }
            };
            mover();
        }
    }
    
    
    private moveDelivery(e: IAngularEvent, pos: PosicionDTO){
        if(this._vehicle && this._map){
            const prevPos = this._vehicle.getPosition();
            const newPos = new google.maps.LatLng(pos.Latitud, pos.Longitud);
            const heading = google.maps.geometry.spherical.computeHeading(prevPos!, newPos);
        
            const iconUrl = this.generateRotatedIcon('/assets/images/moto.svg', heading);
        
            this._vehicle.setPosition(newPos);
            this._vehicle.setIcon({
                url: iconUrl,
                scaledSize: new google.maps.Size(40, 40)
            });

            this._map.panTo({lat:pos.Latitud,lng:pos.Longitud});
        }
    }

    private async assignDelivery(e: IAngularEvent, pos: PosicionDTO){
        if(pos.Latitud == 0 && pos.Longitud == 0){
            const [latT,lngT] = this.EnvironmentServices.RestaurantePosition;
            pos.Latitud = latT;
            pos.Longitud = lngT;
        }

        if(this._map){
            const motoMarker = new google.maps.Marker({
                position: { lat: pos.Latitud, lng: pos.Longitud },
                map: this._map,
                title: 'Moto',
                icon: {
                    url: '/assets/images/moto.svg',
                    scaledSize: new google.maps.Size(40, 40)
                } as google.maps.Icon
            });
            this._vehicle = motoMarker;
            this._deliveryId = pos.DeliveryId;
        }
    }

    private async addRouteToMap(e: IAngularEvent){
        if(this.$element.children(`Map-I-${this._orderId}`).length > 0 && this._latitude != 0.0 && this._longitude != 0.0){
            const directionsService = new google.maps.DirectionsService();
            const directionsRenderer = new google.maps.DirectionsRenderer();
            const [resLat, resLng] = this.EnvironmentServices.RestaurantePosition;

            directionsRenderer.setMap(this._map);
            const request = {
                origin: { lat: resLat, lng: resLng },
                destination: { lat: this._latitude, lng: this._longitude },
                travelMode: google.maps.TravelMode.DRIVING,
            } as google.maps.DirectionsRequest;
            try {
                const result = await directionsService.route(request);
                if(result.routes.length > 0){
                    if(this._simulate){
                        this._simulate = result.routes[0].overview_path;
                    }
                }
                directionsRenderer.setDirections(result);
                this.$element.append(this._mapHtml);
                await this.$scope.$apply();
            } catch (error) {
                console.error('Error al obtener la ruta:', error);
            }
        }
    }

    private async createMap(e: IAngularEvent, id: string){
        if(!id) return;
        
        this._orderId = id;

        if(this.$element.children(`#Map-I-${this._orderId}`).length > 0) return;
        else
        {
            const [resLat, resLng] = this.EnvironmentServices.RestaurantePosition;
            if(!this._map)
            {
                this._mapHtml = $('<div></div>')
                    .attr('id',`#Map-I-${this._orderId}`)
                    .css('width',this._width)
                    .css('height',this._height)
                    .get(0) 
            }
            const mapOptions = {
                zoom: 10,
                center: { lat: resLat, lng:resLng },
                disableDefaultUI: true,
                cameraControl: false,
                fullscreenControl: true
            } as google.maps.MapOptions;
        
            const map = new google.maps.Map(this._mapHtml, mapOptions);
            this._map = map;
            this.$element.append(this._mapHtml);
        }
    }
    
    private removeCurrent(e: IAngularEvent){
        if (this._timeout)
            clearTimeout(this._timeout);
        this._map = null;
        this._mapHtml = null;
        this._timeout = null;
        angular.element(document.getElementById(`#Map-I-${this._orderId}`)).remove();
        this._orderId = null;
        this._deliveryId = null;
    }

    private addMarker(e: IAngularEvent, data: any){
        
    }


    private addButton(e: IAngularEvent, data: MapButtonAddEventData){
        const {callback, classes, height, width, iconUrl, name} = data;
        if(!this._mapCtrl){
            this._mapCtrl = $(`<span id='Map-C-${this._orderId}' class='d-flex gap-1 my-2'></span>`);
            this._map.controls[google.maps.ControlPosition.BOTTOM_LEFT].push(this._mapCtrl.get(0));
        }
        let nameId = `Map-C-B-${this._orderId}`;
        if(this._mapCtrl.children(`#${nameId}`).length == 0){
            let btn = $(`<button id='Map-C-B-${this._orderId}-${name}'></button>`) as JQuery<HTMLButtonElement>;
            if(classes && classes != ''){
                this.$scope.$watch(() => data.classes, (nV) => {
                    btn.attr('class', nV || '');
                });
                btn = btn.addClass(classes);
            }
            if(iconUrl && iconUrl != '') btn = btn.append($(`<img src='${iconUrl}'/>`).css('width',width).css('height',height).get(0));  
            else btn = btn.css('width',width).css('height',height);
            if(callback) btn.on('click', callback);
            this._mapCtrl.append(btn.get(0));
        }
    }

    private removeButton(e: IAngularEvent, name: string){
        let nameId = `Map-C-B-${this._orderId}`;
        if(this._mapCtrl.children(`#${nameId}`)) this._mapCtrl.remove(`#${nameId}`);
    }
}