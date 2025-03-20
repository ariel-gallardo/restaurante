import ClassMap from "@css/ClassMap";
import UserServices from "@services/UserServices";
import MapButtonAddEventData from "@events/MapButtonAddEventData";
import PosicionUsuario from "@models/Posicion/PosicionUsuario";

export default class UserController{
     static $inject = ['$location', '$cookies', 'UserServices', '$rootScope','$scope'];
/**
 * @param {angular.ILocationService} $location
 * @param {angular.cookies.ICookiesService} $cookies
 * @param {UserServices} UserServices
 * @param {angular.IRootScopeService} $rootScope 
 * @param {angular.IScope} $scope  
 */
    constructor($location, $cookies, UserServices, $rootScope, $scope){
          this.mapClass = new ClassMap;
          this.orderId = 'none';
          this.UserServices = UserServices;
          this.cookies = $cookies;
          this.location = $location;
          this.element = angular.element;
          this.editar = {
               nombre: false,
               correo: false,
               domicilio: false,
               telefono: false
          }
          this._positionChanges = false;
          this._editPosition = true;
          this.nombre = '';
          this.apellido = '';
          this.calle = '';
          this.numero = '';
          this.correo = '';
          this.imagenUrl = '';
          this.telefono = '';
          this.$scope = $scope;
          this.$rootScope = $rootScope;
          this.mapScope = null;

          
          $scope.$watch(() => this.PedidoId, (nV,oV) => {
               
               if(this.orderId && this.orderId != '' && nV && nV != '-')
               {
                    this.orderId = nV;
                    this.$rootScope.$applyAsync(x => {
                         if(!this.map){
                              let map = angular.element($('map-google').get(0));
                              map.attr('id',this.orderId);
                              if(map){
                                   this.mapScope = map.isolateScope();
                                   if(this.mapScope){
                                        this.mapScope.$emit('Map_Create',this.orderId);
                                        this.mapScope.$on('Directive_Output', (e, o) => {
                                             if(this._editPosition && o && o['MarkerHouse']){
                                                  let nPos = o['MarkerHouse'].Position;
                                                  if(nPos){
                                                       this.lat = nPos.Latitud;
                                                       this.lng = nPos.Longitud;
                                                       this._positionChanges = true;
                                                  }
                                             }
                                        });
                                        this.mapScope.$on('Scopes_Output', (e, o) => {
                                             if(!this.MarkerHouseScope && o && o['MarkerHouse'] )
                                             {
                                                  this.MarkerHouseScope = o['MarkerHouse']; 
                                                  this.$scope.$on('AddMarkerHouse',this.MarkerHouseScope.$emit('Buttons_Add'));
                                                  this.$scope.$on('RemoveMarkerHouse',this.MarkerHouseScope.$emit('Buttons_Remove'));
                                             }
                                        });
                                        this.mapScope.$applyAsync();
                                   }
                              }
                         }
                    });
               }
               else
                    this.orderId = 'none';
          });

          this.editarPosicion = this.EditarPosicion.bind(this);
    }

     EditarPosicion(){
          console.log(this)
          this._editPosition = !this._editPosition;
          if(this._editPosition) this.$scope.$emit('AddMarkerHouse');
          else this.$scope.$emit('RemoveMarkerHouse');
          return this._editPosition;
     }

     get PedidoId(){
          return this.UserServices.PedidoId;
     }

     get ShowView(){
          return this.UserServices.IsLogged;
     }

    get ImagenUrl(){
          return this.imagenUrl == '' ? this.UserServices.ImagenUrl : this.imagenUrl;
    }

    get Nombre(){
          return this.nombre;
    }

    set Nombre(nombre){
          this.nombre = nombre.trim();
    }

     get Apellido(){
          return this.apellido;
     }

     set Apellido(apellido){
          this.apellido = apellido.trim();
     }

     get NombreCompleto(){
          return this.Nombre && this.Apellido && this.Nombre != '' && this.Apellido != '' ? `${this.Nombre} ${this.Apellido}` : this.UserServices.NombreCompleto;
     }
 
     get Correo(){
          return this.correo && this.correo != '' ? this.correo : this.UserServices.Correo;
     }

     set Correo(correo){
          this.correo = correo;
     }
 
    get Rol(){
         return this.UserServices.Rol;
    }
 
    get Domicilio(){
         return this.Calle != '' && this.Numero != '' ? `${this.Calle} ${this.Numero}` : this.UserServices.Domicilio;
    }

     get Calle(){
          return this.calle;
     }

     set Calle(calle){
          this.calle = calle.trim();
     }

     get Numero(){
          return this.numero;
     }

     set Numero(numero){
          this.numero = numero.trim();
     }

     set Telefono(telefono){
          this.telefono = telefono;
     }
 
    get Telefono(){
         return this.telefono && this.telefono != '' ? this.telefono : this.UserServices.Telefono;
    }
 
    get TiempoExpiracionToken(){
         return this.UserServices.TiempoExpiracionToken;
    }

    get Logout(){
          return this.UserServices.logout();
    }

    get EditarNombre(){
          this.editar = {...this.editar, nombre: true};
    }

    get BlurNombre(){
          if((this.Nombre != '' && this.Apellido != '') || (this.Nombre == '' && this.Apellido == ''))
          this.editar = {
               ...this.editar,
               nombre: false,
          }
    }

    get BlurDomicilio(){
          if((this.Calle != '' && this.Numero != '') || (this.Calle == '' && this.Numero == ''))
          this.editar = {
               ...this.editar,
               domicilio: false,
          }
     }

     get EditarDomicilio(){
          this.editar = {...this.editar, domicilio: true};
     }

     get EditarCorreo(){
          this.editar = {...this.editar, correo: true};
     }

     get BlurCorreo(){
          if(this.correo == '' || this.correo.length > 0)
          this.editar = {
               ...this.editar,
               correo: false,
          }
     }

     get EditarTelefono(){
          this.editar = {...this.editar, telefono: true};
     }

     get BlurTelefono(){
          if(this.telefono == '' || this.telefono.length > 0)
          this.editar = {
               ...this.editar,
               telefono: false,
          }
     }

     get SaveChanges(){
          let empty = true;
          let toSend = {};

          if(this.nombre != '' && this.apellido != '' 
               && `${this.Nombre} ${this.Apellido}` != this.UserServices.NombreCompleto){
               toSend = {...toSend,nombre: this.Nombre, apellido: this.Apellido};
               empty = false;
          }

          if(this.Calle != '' && this.Numero != '' 
               && `${this.Calle} ${this.Numero}` != this.UserServices.Domicilio){
               toSend = {...toSend,calle: this.Calle, numero: this.Numero};
               empty = false;
          }

          if(this.Correo != '' && this.Correo != this.UserServices.Correo) 
          {
               toSend = {...toSend,correo: this.Correo};
               empty = false;
          }

          if(this.lat != '' && this.lat != '-' && this.lng != '' && this.lng != '-' && this._positionChanges)
          {
               toSend = {...toSend, Latitud: this.lat, Longitud: this.lng};
               empty = false;
          }
          
          if(!empty) 
          {
               this.UserServices.update(toSend);
               this._positionChanges = false;
          }
     }

     get lat(){
          let r = this.UserServices.Latitude;
          return  r && r != '-' ? Number(r) : 0.0;
     }

     set lat(nV){
          if(nV && nV != '-' || nV != '') this.UserServices.Latitude = `${nV}`;
     }

     get lng(){
          let r = this.UserServices.Longitude;
          return  r && r != '-' ? Number(r) : 0.0;
     }

     set lng(nV){
          if(nV && nV != '-' || nV != '') this.UserServices.Longitude = `${nV}`;
     }
}