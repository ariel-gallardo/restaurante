import Category from "@models/Category/Category";
import CategoryGetQuerie from "@queries/CategoryGetQuerie";
import Pagination from "@models/Pagination";
import Response from "@models/Response";
import ApiServices from "@services/ApiServices";

export default class CategoryServices{
        private static $inject: ['ApiServices'];
        private _url: string = '/api/categoria';
    
        constructor(private ApiServices: ApiServices) {
    
        }
    
        async getAll(querie: CategoryGetQuerie = new CategoryGetQuerie): Promise<Pagination<Category>>{
            let data = await this.ApiServices.get(`${this._url}${QuerieURLFromObject(querie)}`) as Response<Pagination<Category>>;
            return data?.content ?? new Pagination<Category>;
        }
}