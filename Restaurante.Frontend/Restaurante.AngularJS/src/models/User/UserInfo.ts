import Order from "@models/Order/Order";

export default class UserInfo{
    id: string;
    nombreCompleto: string = '-';
    correo: string = '-';
    tipoDeUsuario: string = '-';
    domicilio: string = '-';
    telefono: string = '-';
    imagenUrl: string = '/assets/images/commonUser.svg';
    tiempoExpiracionToken: string = '-';
    caducaEn: string = '-';
    pedido: Order = new Order;
    pedidoTrabajo: Order[] = [];
    latitud: string = '-';
    longitud: string = '-';
}