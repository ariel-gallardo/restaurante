import ClassMap from "@css/ClassMap";
import OrderDetail from "@models/Order/OrderDetail"

export default class Order{
    data: OrderDetail[] = new Array<OrderDetail>;
    estado: string = 'SEARCHING';
    pedido: string;
    mapClass: ClassMap;
}