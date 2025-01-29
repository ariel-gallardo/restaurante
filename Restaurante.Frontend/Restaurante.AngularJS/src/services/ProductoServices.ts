import Pagination from "@models/Pagination";
import Response from "@models/Response";
import ProductGetQuerie from "@queries/ProductGetQuerie";

import ApiServices from "@services/ApiServices";
import Product from "@models/Product/Product";

export default class ProductoServices{
    private static $inject: ['ApiServices'];
    private _url: string = '/api/producto';

    constructor(private ApiServices: ApiServices) {

    }

    async getAll(querie: ProductGetQuerie = new ProductGetQuerie): Promise<Pagination<Product>>{
        let data = await this.ApiServices.get(`${this._url}${QuerieURLFromObject(querie)}`) as Response<Pagination<Product>>;
        return data?.content ?? new Pagination<Product>;
    }
}