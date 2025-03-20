import MapGoogleController from "@controllers/components/maps/MapGoogleController";
import MapButtonAddEventData from "@events/MapButtonAddEventData";
import PosicionDTO from "@models/Posicion/PosicionDTO";
import PosicionUsuario from "@models/Posicion/PosicionUsuario";
import { IAngularEvent, IAttributes, IRootElementService, IScope } from "angular";

export default class MarkerHouseController{

    private _markerPointerHouseAdded: boolean = false;
    private _markerPointerHouseVisible: boolean = false;
    private _markerPointerHouseButtonsAdded: boolean = false;
    private _markerPointerHouse: google.maps.Marker = null;
    private _map: google.maps.Map = null;
    private _mapScope: IScope = null;
    private _orderId: string = null;
    private _parentCtrl: MapGoogleController;
    private static $inject = ['$scope', '$element', '$attrs'];
    private _position: PosicionUsuario;
    private _btnData: {[key: string]: MapButtonAddEventData} = {};
    private _markerHouse : google.maps.Marker;

    constructor(private $scope: IScope, private $element: IRootElementService, private $attrs: IAttributes) {
        this.$scope.$on('Buttons_Add', this.addButtons.bind(this));
        this.$scope.$on('Buttons_Remove', this.removeButtons.bind(this));

        this.$scope.$watch('ctrl.parentCtrl', (nV: MapGoogleController) =>{
            if(!this._parentCtrl) this._parentCtrl = nV;
        })
        this.$scope.$watchGroup(['ctrl.map', 'ctrl.orderId'],(nV) => {
            const [nMap, nOId] = nV;
            if(!this._map && nMap && nOId)
            {
                this._map = nMap;
                this._orderId = nOId;
                this._mapScope = angular.element($(`#${this._orderId}`).get(0)).isolateScope();
                this.addButtons();
            }
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

    private generateIcon(imageUrl: string, size: number = 40): string {
        const svg = `
            <svg xmlns="http://www.w3.org/2000/svg" width="${size}" height="${size}" viewBox="0 0 ${size} ${size}">
                <image href="${imageUrl}" x="0" y="0" width="${size}" height="${size}" />
            </svg>
        `;
        const svgBase64 = btoa(svg);
        return `data:image/svg+xml;base64,${svgBase64}`;
    }

    private removeButtons(){
        if(this._map && this._markerPointerHouseButtonsAdded){
            this._mapScope.$emit('Map_Button_Remove','NewHousePointer');
            this._mapScope.$emit('Map_Button_Remove','RemoveHousePointer');
            this._markerPointerHouseButtonsAdded = false;
        }
    }

    private addBootstrapClasses(){
        this._btnData = {
            "NewHousePointer": new MapButtonAddEventData(
                this.addMarkerHousePointer.bind(this),
                'NewHousePointer',
                '2rem',
                '2rem',
                'btn btn-light',
                '/assets/images/houseAdd.svg'
           ),
           "RemoveHousePointer": new MapButtonAddEventData(
            this.removeMarkerHousePointer.bind(this),
                'RemoveHousePointer',
                '2rem',
                '2rem',
                'btn btn-light',
                '/assets/images/houseRemove.svg'
            ),
            "ConfirmHousePointer": new MapButtonAddEventData(
                this.cancelMarkerHousePointerChanges.bind(this),
                'ConfirmHousePointer',
                '2rem',
                '2rem',
                'd-none',
                '/assets/images/houseCancel.svg'
            ),
            "CancelHousePointer": new MapButtonAddEventData(
                this.acceptMarkerHousePointerChanges.bind(this),
                'CancelHousePointer',
                '2rem',
                '2rem',
                'd-none',
                '/assets/images/houseOk.svg'
            )
        };
    }
    private addButtons(){
        if(this._map && !this._markerPointerHouseButtonsAdded){
            this.addBootstrapClasses();
            this._mapScope.$emit('Map_Button_Add', this._btnData["NewHousePointer"]);
            //this._mapScope.$emit('Map_Button_Add', this._btnData["RemoveHousePointer"]);
            this._mapScope.$emit('Map_Button_Add', this._btnData["ConfirmHousePointer"]);
            this._mapScope.$emit('Map_Button_Add', this._btnData["CancelHousePointer"]);
            this._markerPointerHouseButtonsAdded = true;
        }
    }

    private addFunctions(){
        if(this._markerPointerHouseAdded){
            google.maps.event.addListener(this._markerPointerHouse, 'dragend', () => {
                const newPosition = this._markerPointerHouse.getPosition();
                this._position = new PosicionUsuario(newPosition);
            });
        }

    }

    private addMarkerHousePointer() {
        if (!this._markerPointerHouseAdded) {

          this._btnData["ConfirmHousePointer"].classes = 'btn btn-light';
          this._btnData["CancelHousePointer"].classes = 'btn btn-light';
          this._btnData["NewHousePointer"].classes = 'd-none';
          
          const position = this._map.getCenter();
          this._markerPointerHouse = new google.maps.Marker({
            position: position,
            map: this._map,
            label: 'C',
            draggable: true,
          });
          this._markerPointerHouseAdded = true;
          this._markerPointerHouseVisible = true;
          this.addFunctions();
        }
    }
    

    private addMarkerHouse(){
        if (!this._markerHouse && this._position){
            if(this._position.Latitud != 0 && this._position.Longitud != 0)
                this._markerHouse = new google.maps.Marker({
                    position: { lat: this._position.Latitud, lng: this._position.Longitud },
                    map: this._map,
                    title: 'House',
                    icon: {
                        url: '/assets/images/house.svg',
                        scaledSize: new google.maps.Size(40, 40)
                    } as google.maps.Icon
                });
        }else if(this._position.Latitud != 0 && this._position.Longitud != 0){
            this._markerHouse.setPosition({ lat: this._position.Latitud, lng: this._position.Longitud });
        }
    }

    private removeMarkerHousePointer() {
        if (this._markerPointerHouseAdded && this._markerPointerHouse) {
          this._markerPointerHouse.setMap(null);
          this._markerPointerHouseButtonsAdded = false;
          this._markerPointerHouseAdded = false;
          this._markerPointerHouseVisible = false;
          this._btnData["ConfirmHousePointer"].classes = 'd-none';
          this._btnData["CancelHousePointer"].classes = 'd-none';
          this._btnData["NewHousePointer"].classes = 'btn btn-light';
        }
    }

    private acceptMarkerHousePointerChanges(){
        if(this._position)
        {
            this._btnData["ConfirmHousePointer"].classes = 'd-none';
            this._btnData["CancelHousePointer"].classes = 'd-none';
            this._btnData["NewHousePointer"].classes = 'btn btn-light';
            this._parentCtrl.AddDirectiveOutput('MarkerHouse',{
                Position: this._position
            });
            this.removeMarkerHousePointer();
            this.addMarkerHouse();
        }
    }

    private cancelMarkerHousePointerChanges(){
        this._position = null;
        this.removeMarkerHousePointer();
    }
}