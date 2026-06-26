import { createAction, props } from '@ngrx/store';

export interface SharedUser {
  nombreCompleto: string;
  correo: string;
  tipoDeUsuario: string;
  imagenUrl?: string;
  domicilio?: string;
  telefono?: string;
}

export const sharedShellEventTracked = createAction(
  '[Shared Shell] Event Tracked',
  props<{ source: 'v1' | 'v2' }>()
);

export const setOrderStatus = createAction(
  'ORDER_STATUS/SET',
  props<{ payload: string }>()
);

export const setSharedUser = createAction(
  '[Shared Shell] Set User',
  props<{ user: SharedUser }>()
);

export const clearSharedUser = createAction(
  '[Shared Shell] Clear User'
);

export const setAuthUser = createAction(
  'AUTH/SET_USER',
  props<{ payload: SharedUser }>()
);

export const authLogout = createAction(
  'AUTH/LOGOUT'
);

export const setCartCount = createAction(
  'CART/SET_COUNT',
  props<{ payload: number }>()
);

