import ApiServices from "@services/ApiServices";
import EnvironmentServices from "@services/EnvironmentServices";
import { IAngularEvent, IRootScopeService } from "angular";

export default class GoogleMapsServices{

    static $inject = ['$rootScope', 'EnvironmentServices', 'ApiServices', 'NgMap'];

    private _maps: {};
    private _mapsHtml: {};
    private _delivery: {};
    private _timeouts: {};
    private _simulate: {};

    constructor(private $rootScope: IRootScopeService, private EnvironmentServices: EnvironmentServices, private ApiServices: ApiServices, private NgMap: ng.map.INgMap) {
        this._maps = {};
        this._mapsHtml= {};
        this._delivery = {};
        this._timeouts = {};
        this._simulate = {};

        this.$rootScope.$on('LoadMap',this.createMap.bind(this));
        this.$rootScope.$on('AssignDelivery',this.assignDelivery.bind(this));
        this.$rootScope.$on('RemoveClient',this.removeCurrent.bind(this));
        this.$rootScope.$on('MoveDelivery',this.moveDelivery.bind(this));
        this.$rootScope.$on('SimulateDelivery',this.simulateDelivery.bind(this));
        this.$rootScope.$on('DeliveryFinished',this.removeCurrent.bind(this));
    }

    private simulateDelivery(e: IAngularEvent, clientId: string) {
        let index = 0;
        const delay = 300;
        let timeoutId: number | undefined;
        let path: google.maps.LatLng[] = this._simulate[clientId];
        if(path){
            const mover = () => {
                if (index < path.length) {
                    const point = path[index];
                    console.log(`Point ${index} - ${point.lat().toFixed(5)} - ${point.lng().toFixed(5)}`);
                    this.$rootScope.$emit('MoveDelivery', clientId, point.lat(), point.lng());
                    index++;
                    this._timeouts[clientId] = setTimeout(mover, delay);
                } else {
                    this.$rootScope.$emit('DeliveryFinished', clientId);
                }
            };
            mover();
        }
    }
    
    
    

    private moveDelivery(e: IAngularEvent, clientId: string, lat: number, lng: number){
        if(this._delivery[clientId] && this._maps[clientId]){
            (this._delivery[clientId] as google.maps.Marker)
            .setPosition({lat, lng});
            (this._maps[clientId] as google.maps.Map)
            .panTo({lat,lng});
        }
    }

    private async assignDelivery(e: IAngularEvent, clientId: string, lat: number, lng: number){
        const cMap: google.maps.Map = this._maps[clientId];
        if(cMap){
            const motoMarker = new google.maps.Marker({
                position: { lat, lng },
                map: cMap,
                title: 'Moto',
                icon: {
                    url: '/assets/images/moto.svg',
                    scaledSize: new google.maps.Size(40, 40)
                } as google.maps.Icon
            });
            this._delivery = {...this._delivery, [clientId]: motoMarker};
            this.$rootScope.$emit('SimulateDelivery',clientId)
        }
    }


    private async createMap(e: IAngularEvent, clientId: string, appendOn: string){
        if($(`#Map-${clientId}`).length > 0) return;
        else{

            if(!this._maps[clientId])
            {
                this._mapsHtml = {...this._mapsHtml, [clientId]: $('<div></div>')
                    .attr('id',`#Map-${clientId}`)
                    .addClass('maps')
                    .get(0) 
                }
            }
            
            const directionsService = new google.maps.DirectionsService();
            const directionsRenderer = new google.maps.DirectionsRenderer();
        
            const mapOptions = {
                zoom: 10,
                center: { lat:-32.906055874673235, lng:-68.80137674694244 },
                disableDefaultUI: true,
                cameraControl: false,
                fullscreenControl: true
            } as google.maps.MapOptions;
        
            const map = new google.maps.Map(this._mapsHtml[clientId], mapOptions);
            this._maps = {...this._maps, [clientId]: map};

            if($(appendOn).length > 0 && $(appendOn).children(`Map-${clientId}`).length == 0){
                directionsRenderer.setMap(map);
                const request = {
                    origin: { lat:-32.906055874673235, lng:-68.80137674694244 },
                    destination: { lat: -32.891898, lng: -68.846526 },
                    travelMode: google.maps.TravelMode.DRIVING,
                } as google.maps.DirectionsRequest;
                try {
                    const result = await directionsService.route(request);
                    if(result.routes.length > 0){
                        console.log(result)
                        if(!this._simulate[clientId]){
                            this._simulate = {...this._simulate, [clientId]: result.routes[0].overview_path}
                        }
                    }
                    directionsRenderer.setDirections(result);
                    $(appendOn).append(this._mapsHtml[clientId]);
                    await this.$rootScope.$apply();
                } catch (error) {
                    console.error('Error al obtener la ruta:', error);
                }
            }
            this.$rootScope.$emit('AssignDelivery',clientId,-32.906055874673235,-68.80137674694244);
        }
    }

    private removeCurrent(clientId: string, childrenOf: string){
        console.log('Delivery End Travel')
        if (this._timeouts[clientId])
            clearTimeout(this._timeouts[clientId]);
        delete this._delivery[clientId];
        delete this._maps[clientId];
        delete this._mapsHtml[clientId];
        delete this._timeouts[clientId];
        let children = $(childrenOf);
        if(children.length > 0) children.remove(`#Map-${clientId}`);
    }
    
    
}