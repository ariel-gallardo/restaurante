import GeoRefServices from "@services/GeoRefServices";
import angular from "angular";

export default class RegisterController{

    /**
     * @param {UserServices} UserServices,
     * @param {angular.ILocationService} $location,
     * @param {angular.IRootScopeService} $scope,
     * @param {GeoRefServices} GeoRefServices,
    */
    constructor(UserServices, $location, $scope, GeoRefServices){
        this.GeoRefServices = GeoRefServices;
        this.FirstTimeLoad = true;
        this.location = $location;
        this.UserServices = UserServices;
        this.scope = $scope;
        this.departamentos = [];
        this.localidades = [];
        this.name = '';
        this.surname = '';
        this.email = '';
        this.password = '';
        this.re_password = '';
        this.codearea = '';
        this.phone = '';
        this.street = '';
        this.numstreet = '';
        this.LocalidadActual = '';

        this.scope.$on('firstTimeLoad',async () => {
            this.departamentos = await this.GeoRefServices.Departamentos;
            await this.scope.$applyAsync();
        });

        this.scope.$on('recargarLocalidades', async () => {
            this.localidades = await this.GeoRefServices.Localidades;
            await this.scope.$applyAsync();
        });

        if(this.FirstTimeLoad){
            this.scope.$emit('firstTimeLoad');
            this.FirstTimeLoad = false;
        }
    }



    set DepartamentoActual(depto){
        this.GeoRefServices.DepartamentoActual = depto;
        this.scope.$emit('recargarLocalidades');
    }

    get DepartamentoActual(){
        return this.GeoRefServices.DepartamentoActual;
    }

    get ShowView(){
        return !this.UserServices.IsLogged;
    }

    get RegisterUser(){
        let empty = true;
        let toSend = {};
        if(this.name != ''){
            toSend = {...toSend, Nombre: this.name};
            empty = false;
        }
        if(this.surname != '') {
            toSend = {...toSend, Apellido: this.surname};
            empty = false;
        }
        if(this.email != ''){
            toSend = {...toSend, Correo: this.email};
            empty = false;
        }
        if(this.password != '') {
            toSend = {...toSend, Password: this.password};
            empty = false;
        }
        if(this.re_password != '') {
            toSend = {...toSend, RePassword: this.re_password};
            empty = false;
        }
        if(this.codearea != '') {
            toSend = {...toSend, CodigoArea: this.codearea};
            empty = false;
        }
        if(this.phone != '') {
            toSend = {...toSend, NumeroTelefono: this.phone};
            empty = false;
        }
        if(this.street != '') {
            toSend = {...toSend, Calle: this.street};
            empty = false;
        }
        if(this.numstreet != '') {
            toSend = {...toSend, Numero: this.numstreet};
            empty = false;
        }
        if(this.LocalidadActual != '') {
            toSend = {...toSend, Localidad: this.LocalidadActual};
            empty = false;
        }
        this.UserServices.register(toSend);
    }
    
    vRange = {
        StreetNum: () => {
            let num = Number(this.numstreet);
            if(isNaN(num)) this.numstreet = '';
            else if(num < 1)
                this.numstreet = 1;
            else if(num > 10000)
                this.numstreet = 10000;
        },
        CodeArea: () => {
            let num = Number(this.codearea);
            if(isNaN(num)) this.codearea = '';
            else if(num < 1)
                this.codearea = 1;
            else if(num > 10000)
                this.codearea = 10000;
        },
        Phone: () => {
            let num = Number(this.phone);
            if(isNaN(num)) this.phone = '';
            else if(num < 1)
                this.phone = 1;
            else if(num > 9999999)
                this.phone = 9999999;
        }
    }
}