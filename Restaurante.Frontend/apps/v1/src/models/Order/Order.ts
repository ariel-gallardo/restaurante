import OrderDetail from "@models/Order/OrderDetail"
import ORDER_STATUS, { OrderStatus } from "@models/Order/OrderStatus";

export default class Order{
    data: OrderDetail[] = new Array<OrderDetail>;
    estado: OrderStatus = ORDER_STATUS.SEARCHING;
    pedido: string;
}