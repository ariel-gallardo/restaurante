import OrderAction from "@models/Order/OrderAction";
import ProductStoreMin from "@models/Product/ProductStoreMin";

export default class OrderInteraction<T>{
    Action: OrderAction = OrderAction.Ninguna;
    Data: T = null;
    public static Adicionar(id: string){
        let i = new OrderInteraction;
        let data = new ProductStoreMin;
        data.id = id;
        i.Data = data;
        i.Action = OrderAction.Adicionar;
        return i;
    }
    public static Quitar(id: string){
        let i = new OrderInteraction;
        let data = new ProductStoreMin;
        data.id = id;
        i.Data = data;
        i.Action = OrderAction.Quitar;
        return i;
    }
    public static Remover(id: string){
        let i = new OrderInteraction;
        let data = new ProductStoreMin;
        data.id = id;
        i.Data = data;
        i.Action = OrderAction.Remover;
        return i;
    }
    public static Realizar(){
        let i = new OrderInteraction;
        i.Action = OrderAction.Realizar;
        return i;
    }
    public static Cancelar(){
        let i = new OrderInteraction;
        i.Action = OrderAction.Cancelar;
        return i;
    }
    public static Delivery(){
        let i = new OrderInteraction;
        i.Action = OrderAction.Delivery;
        return i;
    }
    public static Puerta(){
        let i = new OrderInteraction;
        i.Action = OrderAction.Puerta;
        return i;
    }
    public static Preparar(){
        let i = new OrderInteraction;
        i.Action = OrderAction.Preparar;
        return i;
    }
    public static Recepcion(){
        let i = new OrderInteraction;
        i.Action = OrderAction.Recepcion;
        return i;
    }
}