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

    private removeButtons(){
        if(this._map && this._markerPointerHouseButtonsAdded){
            this._mapScope.$emit('Map_Button_Remove','NewHousePointer');
            this._mapScope.$emit('Map_Button_Remove','RemoveHousePointer');
            this._markerPointerHouseButtonsAdded = false;
        }
    }

    private addButtons(){
        if(this._map && !this._markerPointerHouseButtonsAdded){
            this._mapScope.$emit('Map_Button_Add',
                new MapButtonAddEventData(
                     this.addMarkerHousePointer.bind(this),
                     'NewHousePointer',
                     '2rem',
                     '2rem',
                     'btn btn-info',
                     '/assets/images/houseAdd.svg'
                ));
                this._mapScope.$emit('Map_Button_Add',
                     new MapButtonAddEventData(
                          this.removeMarkerHousePointer.bind(this),
                          'RemoveHousePointer',
                          '2rem',
                          '2rem',
                          'btn btn-info',
                          '/assets/images/houseRemove.svg'
                ));
                this._markerPointerHouseButtonsAdded = true;
        }
    }

    private addFunctions(){
        if(this._markerPointerHouseAdded){
            google.maps.event.addListener(this._markerPointerHouse, 'dragend', () => {
                const newPosition = this._markerPointerHouse.getPosition();
                this._parentCtrl.AddDirectiveOutput('MarkerHouse',{
                    Position: new PosicionUsuario(newPosition)
                });
            });
        }

    }

    private addMarkerHousePointer() {
        if (!this._markerPointerHouseAdded) {
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

    private removeMarkerHousePointer() {
        if (this._markerPointerHouseAdded && this._markerPointerHouse) {
          this._markerPointerHouse.setMap(null);
          this._markerPointerHouseButtonsAdded = false;
          this._markerPointerHouseAdded = false;
          this._markerPointerHouseVisible = false;
        }
    }
}