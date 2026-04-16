import Pagination from "@models/Pagination";
import Response from "@models/Response";
import ProductGetQuerie from "@queries/ProductGetQuerie";

import ApiServices from "@services/ApiServices";
import Product from "@models/Product/Product";

export default class ProductoServices{
    static $inject = ['ApiServices'];
    private _url: string = '/api/producto';

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

    async getAll(querie: ProductGetQuerie = new ProductGetQuerie): Promise<Pagination<Product>>{
        let data = await this.ApiServices.get(`${this._url}${this.buildQueryFromObject(querie)}`) as Response<Pagination<Product>>;
        return data?.content ?? new Pagination<Product>;
    }
}