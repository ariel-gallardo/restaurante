import ORDER_STATUS, { OrderStatus } from '@models/Order/OrderStatus';

export const SET_ORDER_STATUS = 'ORDER_STATUS/SET';

export interface OrderStatusState {
    ORDER_STATUS: OrderStatus;
}

export interface SetOrderStatusAction {
    type: typeof SET_ORDER_STATUS;
    payload: OrderStatus;
}

export type OrderStatusActions = SetOrderStatusAction;

const initialState: OrderStatusState = {
    ORDER_STATUS: ORDER_STATUS.SEARCHING,
};

export function orderStatusReducer(
    state: OrderStatusState = initialState,
    action: OrderStatusActions
): OrderStatusState {
    switch (action.type) {
        case SET_ORDER_STATUS:
            return {
                ...state,
                ORDER_STATUS: action.payload,
            };
        default:
            return state;
    }
}

export function setOrderStatus(status: OrderStatus): SetOrderStatusAction {
    return {
        type: SET_ORDER_STATUS,
        payload: status,
    };
}
