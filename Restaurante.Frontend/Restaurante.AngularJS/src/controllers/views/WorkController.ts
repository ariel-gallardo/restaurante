import ClassMap from "@css/ClassMap";
import Order from "@models/Order/Order";
import Pagination from "@models/Pagination";
import OrderGetQuerie from "@queries/OrderGetQuerie";
import PedidosServices from "@services/PedidosServices";
import { IRootScopeService } from "angular";

export default class WorkController{
    private _permissions: [];
    private _querie : OrderGetQuerie = new OrderGetQuerie;
    private _pagination: Pagination<Order>;
    private _data: Order[];

    static $inject = ['$rootScope', 'PedidosServices'];

    constructor(private $rootScope : IRootScopeService, private PedidosServices: PedidosServices) {
        this.PedidosServices.GetByQuerie(this._querie).then(x => {
            this._pagination = x?.content ?? new Pagination<Order>;
            this._data = this._pagination?.content?.map(x => {
                x.mapClass = new ClassMap;
                return x;
            }) ?? [];
            this.onViewMap.bind(this);
            this.onAssignDelivery.bind(this);
            this.onViewDetails.bind(this);
            this.onModifyDetails.bind(this);
            this.onCancel.bind(this);
        });
    }

    public get Data(): Order[]{
        return this._data;
    }

    public get Permissions(){
        return this._permissions;
    }

    public onViewMap(order: Order){
        console.log('onViewMap');
    }
    public onAssignDelivery(order: Order){
        console.log('onAssignDelivery');
    }
    public onViewDetails(order: Order){
        console.log('onViewDetails');
    }
    public onModifyDetails(order: Order){
        console.log('onModifyDetails');
    }
    public onCancel(order: Order){
        console.log('onCancel');
    }
}