import Category from "@models/Category/Category";
import CategoryGetQuerie from "@queries/CategoryGetQuerie";
import Pagination from "@models/Pagination";
import Response from "@models/Response";
import ApiServices from "@services/ApiServices";

export default class CategoryServices{
    static $inject = ['ApiServices'];
        private _url: string = '/api/categoria';

        private buildQueryFromObject(querie: { [key: string]: any }): string {
            const data = querie
                ? Object.entries(querie)
                    .map(([k, v]) => (v ? [String(k), String(v)] : null))
                    .filter((x) => x != null)
                : [];

            return data.length > 0 ? `?${new URLSearchParams(data as string[][]).toString()}` : '';
        }
    
        constructor(private ApiServices: ApiServices) {
    
        }
    
        async getAll(querie: CategoryGetQuerie = new CategoryGetQuerie): Promise<Pagination<Category>>{
            let data = await this.ApiServices.get(`${this._url}${this.buildQueryFromObject(querie)}`) as Response<Pagination<Category>>;
            return data?.content ?? new Pagination<Category>;
        }
}