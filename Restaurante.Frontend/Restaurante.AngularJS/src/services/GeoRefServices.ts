import { DepartamentosDTO, DepartamentoDTO } from "@models/DepartamentoDTO";
import {LocalidadesDTO, LocalidadDTO} from "@models/LocalidadDTO";
import ApiServices from "@services/ApiServices";
import EnvironmentServices from "@services/EnvironmentServices";

export default class GeoRefServices{

    private _localidadUrl : string;
    private _departamentosUrl : string;
    private _localidades : LocalidadDTO[] = [];
    private _departamentos : DepartamentoDTO[] = [];
    private _cambioDpto : boolean = false;
    private _dptoActual : string = '';
     
    constructor(private EnvironmentServices: EnvironmentServices, private ApiServices: ApiServices) {
        this._departamentosUrl = `https://apis.datos.gob.ar/georef/api/departamentos?provincia=${this.EnvironmentServices.Provincia}&campos=nombre&max=100`;
    }

    private async GetDepartamentos(){
        let data = await this.ApiServices.get(this._departamentosUrl) as DepartamentosDTO;
        return data.departamentos;
    }

    private LocalidadUrl(dpto:string){
        return `https://apis.datos.gob.ar/georef/api/localidades?provincia=${this.EnvironmentServices.Provincia}&departamento=${dpto}&campos=nombre`
    }

    private async GetLocalidades(){
        let data = await this.ApiServices.get(this.LocalidadUrl(this.DepartamentoActual)) as LocalidadesDTO;
        return data.localidades;
    }

    public get DepartamentoActual(){
        return this._dptoActual;
    }

    public set DepartamentoActual(nuevoDpto:string){
        if(nuevoDpto && nuevoDpto != '' && nuevoDpto != this._dptoActual){
            this._dptoActual = nuevoDpto;
            this._cambioDpto = true;
        }
    }

    public get Departamentos() : Promise<DepartamentoDTO[]>{
        return new Promise((resolve,reject) => {
            if(this._departamentos.length > 0) resolve(this._departamentos);
            else{
                this.GetDepartamentos()
                .then(result => {
                    this._departamentos = result;
                    resolve(this._departamentos)
                })
                .catch(error => reject(this._departamentos));
            }
        });  
    }

    public get Localidades() : Promise<LocalidadDTO[]>{
        return new Promise((resolve,reject) => {
            if(this._localidades.length > 0 && !this._cambioDpto) resolve(this._localidades);
            else if(
                (this._cambioDpto || this._localidades.length == 0) 
                && (this._dptoActual && this._dptoActual != '')
            ){
                this.GetLocalidades()
                .then(result => {
                    this._localidades = result;
                    resolve(this._localidades)
                })
                .catch(error => reject(this._localidades));
            }else{
                resolve(this._localidades);
            }
        });  
    }
}