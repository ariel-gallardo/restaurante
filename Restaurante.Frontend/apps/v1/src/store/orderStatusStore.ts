import ORDER_STATUS, { OrderStatus } from '@models/Order/OrderStatus';

export const SET_ORDER_STATUS = 'ORDER_STATUS/SET';
export const SET_THEME = 'THEME/SET';
export const SET_LANGUAGE = 'LANGUAGE/SET';

export interface OrderStatusState {
    ORDER_STATUS: OrderStatus;
    theme: 'light' | 'dark';
    language: 'es' | 'en';
}

export interface SetOrderStatusAction {
    type: typeof SET_ORDER_STATUS;
    payload: OrderStatus;
}

export interface SetThemeAction {
    type: typeof SET_THEME;
    payload: 'light' | 'dark';
}

export interface SetLanguageAction {
    type: typeof SET_LANGUAGE;
    payload: 'es' | 'en';
}

export type OrderStatusActions = SetOrderStatusAction | SetThemeAction | SetLanguageAction;

const getInitialTheme = (): 'light' | 'dark' => {
    try {
        if (typeof localStorage !== 'undefined') {
            const theme = localStorage.getItem('theme');
            if (theme === 'light' || theme === 'dark') return theme;
        }
    } catch(e) {}
    return 'light';
};

const getInitialLanguage = (): 'es' | 'en' => {
    try {
        if (typeof localStorage !== 'undefined') {
            const lang = localStorage.getItem('language');
            if (lang === 'es' || lang === 'en') return lang;
        }
    } catch(e) {}
    return 'es';
};

const initialState: OrderStatusState = {
    ORDER_STATUS: ORDER_STATUS.SEARCHING,
    theme: getInitialTheme(),
    language: getInitialLanguage(),
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
        case SET_THEME:
            try {
                if (typeof localStorage !== 'undefined') {
                    localStorage.setItem('theme', action.payload);
                }
            } catch(e) {}
            return {
                ...state,
                theme: action.payload,
            };
        case SET_LANGUAGE:
            try {
                if (typeof localStorage !== 'undefined') {
                    localStorage.setItem('language', action.payload);
                }
            } catch(e) {}
            return {
                ...state,
                language: action.payload,
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

export function setSharedTheme(theme: 'light' | 'dark'): SetThemeAction {
    return {
        type: SET_THEME,
        payload: theme,
    };
}

export function setSharedLanguage(lang: 'es' | 'en'): SetLanguageAction {
    return {
        type: SET_LANGUAGE,
        payload: lang,
    };
}
