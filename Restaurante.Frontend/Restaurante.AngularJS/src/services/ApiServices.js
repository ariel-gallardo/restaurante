 export default class ApiServices {

    /**
     * @param {angular.IHttpService} $http 
     */
    constructor($http) {
        this.http = $http;
    }

    /**
     * @param {string} url 
     * @param {angular.IRequestShortcutConfig} config 
    */
    async get(url, config = null){
        try{
            let result = await this.http.get(url, config);
            return await result.data;
        }catch(e){
            return await e.data;
        }
    }

   /**
     * @param {string} url 
     * @param {any} data 
     * @param {angular.IRequestShortcutConfig} config 
    */
    async post(url,data,config = null){
        try{
            let result = await this.http.post(url, data, config);
            return await result.data;
        }catch(e){
            return await e.data;
        }
    }

   /**
     * @param {string} url 
     * @param {any} data 
     * @param {angular.IRequestShortcutConfig} config 
    */
   async put(url,data,config = null){
        try{
            let result = await this.http.put(url, data, config);
            return await result.data;
        }catch(e){
            return await e.data;
        }
    }

    /**
     * @param {string} url 
     * @param {any} data 
     * @param {angular.IRequestShortcutConfig} config 
    */
   async patch(url,data,config = null){
        try{
            let result = await this.http.patch(url, data, config);
            return await result.data;
        }catch(e){
            return await e.data;
        }
    }

   /**
    * @param {string} url 
    * @param {angular.IRequestShortcutConfig} config 
   */
   async delete(url,data,config = null){
        try{
            let result = await this.http.delete(url, config);
            return await result.data;
        }catch(e){
            return await e.data;
        }
    }
}