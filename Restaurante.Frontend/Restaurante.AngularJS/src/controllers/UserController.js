export default class UserController{

    constructor($location, $cookies, UserServices, $scope){
          this.UserServices = UserServices;
          this.cookies = $cookies;
          this.location = $location;
          this.element = angular.element;

          $scope.$on('$viewContentLoaded',() => {
               this.UserServices.InitUserServices();
          });
          $scope.$on('$destroy',() => {
               this.UserServices.DestroyUserServices();
          });
          this.editar = {
               nombre: false,
               correo: false,
               domicilio: false,
               telefono: false
          }
          this.nombre = '';
          this.apellido = '';
          this.calle = '';
          this.numero = '';
          this.correo = '';
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
          console.log();
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
}