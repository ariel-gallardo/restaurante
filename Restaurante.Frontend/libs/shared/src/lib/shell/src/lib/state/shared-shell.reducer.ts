import { createReducer, on } from '@ngrx/store';
import {
  sharedShellEventTracked,
  setOrderStatus,
  setSharedUser,
  clearSharedUser,
  setAuthUser,
  authLogout,
  setCartCount,
  SharedUser
} from './shared-shell.actions';

export const sharedShellFeatureKey = 'sharedShell';

export interface SharedShellState {
  ORDER_STATUS: string;
  totalEvents: number;
  bySource: {
    v1: number;
    v2: number;
  };
  user: SharedUser | null;
  isAuthenticated: boolean;
  cartCount: number;
}

const getInitialUser = (): SharedUser | null => {
  try {
    if (typeof window !== 'undefined' && window.localStorage) {
      const userJson = localStorage.getItem('userInfo');
      if (userJson) {
        const parsed = JSON.parse(userJson);
        if (parsed && parsed.correo && parsed.correo !== '-') {
          return parsed;
        }
      }
    }
  } catch (e) {
    console.error('Error al parsear el usuario inicial en localStorage', e);
  }
  return null;
};

const getInitialIsAuthenticated = (): boolean => {
  try {
    if (typeof document !== 'undefined') {
      const match = document.cookie.match(new RegExp('(^| )auth_token=([^;]*)'));
      return !!match && !!getInitialUser();
    }
  } catch (e) {
    console.error('Error al comprobar token de autenticación inicial', e);
  }
  return false;
};

const getInitialCartCount = (): number => {
  try {
    if (typeof window !== 'undefined' && window.localStorage) {
      const userJson = localStorage.getItem('userInfo');
      if (userJson) {
        const parsed = JSON.parse(userJson);
        const data = parsed?.pedido?.data;
        let sum = 0;
        if (data) {
          for (const x of data) {
            sum += x.cantidad || 0;
          }
        }
        return sum;
      }
    }
  } catch (e) {
    console.error('Error al calcular el count de carrito inicial', e);
  }
  return 0;
};

const initialState: SharedShellState = {
  ORDER_STATUS: 'SEARCHING',
  totalEvents: 0,
  bySource: {
    v1: 0,
    v2: 0,
  },
  user: getInitialUser(),
  isAuthenticated: getInitialIsAuthenticated(),
  cartCount: getInitialCartCount(),
};

export const sharedShellReducer = createReducer(
  initialState,
  on(sharedShellEventTracked, (state, { source }) => ({
    ...state,
    totalEvents: state.totalEvents + 1,
    bySource: {
      ...state.bySource,
      [source]: state.bySource[source] + 1,
    },
  })),
  on(setOrderStatus, (state, { payload }) => ({
    ...state,
    ORDER_STATUS: payload,
  })),
  on(setSharedUser, (state, { user }) => ({
    ...state,
    user,
    isAuthenticated: true,
  })),
  on(clearSharedUser, (state) => ({
    ...state,
    user: null,
    isAuthenticated: false,
    cartCount: 0,
  })),
  on(setAuthUser, (state, { payload }) => ({
    ...state,
    user: payload,
    isAuthenticated: true,
    cartCount: getInitialCartCount(),
  })),
  on(authLogout, (state) => ({
    ...state,
    user: null,
    isAuthenticated: false,
    cartCount: 0,
  })),
  on(setCartCount, (state, { payload }) => ({
    ...state,
    cartCount: payload,
  }))
);

