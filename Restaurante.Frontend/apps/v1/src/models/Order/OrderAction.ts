enum OrderAction{
    Ninguna = 'NONE',
    Buscar = 'SEARCHING',
    Adicionar = 'ADD',
    Quitar = 'MINUS',
    Realizar = 'CREATED',
    Preparar = 'PREPAIRING',
    Delivery = 'DELIVERY',
    Puerta = 'CLIENT_DOOR',
    Recepcion = 'RECEPTION',
    Entregar = 'TO_CLIENT',
    Cancelar = 'CANCEL',
    Cancelado = 'CANCELED',
    Entregado = 'DONE',
    Remover = 'REMOVE'
}

export default OrderAction;