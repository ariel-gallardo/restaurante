import Pagination from "@models/Pagination";
import Response, { GenericResponse } from "@models/Response";
import { IHttpService, IRequestShortcutConfig } from "angular";

 export default class ApiServices {

    constructor(private $http: IHttpService) {

    }

    async get<T>(url: string, config: IRequestShortcutConfig = null) : Promise<any|GenericResponse|Response<T|Response<Pagination<T>>>|any>{
        try{
            let result = await this.$http.get(url, config);
            return await result.data as T;
        }catch(e){
            return await e.data;
        }
    }


    async post<T>(url:string,data,config: IRequestShortcutConfig = null): Promise<any|GenericResponse|Response<T|Response<Pagination<T>>>|any>{
        try{
            let result = await this.$http.post(url, data, config);
            return await result.data as T;
        }catch(e){
            return await e.data;
        }
    }


   async put<T>(url: string, data,config: IRequestShortcutConfig = null): Promise<any|GenericResponse|Response<T|Response<Pagination<T>>>|any>{
        try{
            let result = await this.$http.put(url, data, config);
            return await result.data as T;
        }catch(e){
            return await e.data;
        }
    }

   async patch<T>(url: string,data,config: IRequestShortcutConfig = null): Promise<any|GenericResponse|Response<T|Response<Pagination<T>>>|any>{
        try{
            let result = await this.$http.patch(url, data, config);
            return await result.data as T;
        }catch(e){
            return await e.data;
        }
    }


   async delete<T>(url: string,data,config: IRequestShortcutConfig = null): Promise<any|GenericResponse|Response<T|Response<Pagination<T>>>|any>{
        try{
            let result = await this.$http.delete(url, config);
            return await result.data as T;
        }catch(e){
            return await e.data;
        }
    }
}