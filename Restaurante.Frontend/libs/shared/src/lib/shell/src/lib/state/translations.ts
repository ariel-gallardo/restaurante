export interface TranslationKeys {
  ORDER_STATUS: string;
  CURRENT_ORDER: string;
  CART: string;
  PROFILE: string;
  MY_PROFILE: string;
  LOGOUT: string;
  EMAIL: string;
  PASSWORD: string;
  CREATE_ACCOUNT: string;
  LOGIN: string;
  REGISTER: string;
  NAME: string;
  SURNAME: string;
  CODEAREA: string;
  PHONE: string;
  STREET: string;
  NUMBER: string;
  SELECT: string;
  DEPARTAMENT: string;
  LOCALITY: string;
  RE_PASSWORD: string;
  REQUEST: string;
  CANCEL: string;
  SEARCHING: string;
  CREATED: string;
  PREPAIRING: string;
  RECEPTION: string;
  DELIVERY: string;
  CLIENT_DOOR: string;
  DONE: string;
  CANCELED: string;
  ABOUT_US: string;
  CONTACT: string;
  RIGHTS: string;
  DESCRIPTION: string;
  WELCOME: string;
  REGISTER_SUBTITLE: string;
  SAVE: string;
  SESSION_END: string;
  FULL_NAME: string;
  MAIL: string;
  FULL_ADDRESS: string;
  ROLE: string;
  THEME_LIGHT: string;
  THEME_DARK: string;
  ITEMS: string;
  EMPTY_CART: string;
  TOTAL: string;
  ALREADY_HAVE_ACCOUNT: string;
}

export const TRANSLATIONS: Record<'es' | 'en', TranslationKeys> = {
  es: {
    ORDER_STATUS: 'Estado del Pedido',
    CURRENT_ORDER: 'Pedido Actual',
    CART: 'Carrito',
    PROFILE: 'Perfil',
    MY_PROFILE: 'Mi Perfil',
    LOGOUT: 'Cerrar Sesión',
    EMAIL: 'Correo Electrónico',
    PASSWORD: 'Contraseña',
    CREATE_ACCOUNT: 'Crear Cuenta',
    LOGIN: 'Iniciar Sesión',
    REGISTER: 'Registrarse',
    NAME: 'Nombre',
    SURNAME: 'Apellido',
    CODEAREA: 'Cód. Área',
    PHONE: 'Teléfono',
    STREET: 'Calle',
    NUMBER: 'Número',
    SELECT: 'Seleccionar',
    DEPARTAMENT: 'Departamento',
    LOCALITY: 'Localidad',
    RE_PASSWORD: 'Repetir Contraseña',
    REQUEST: 'Realizar Pedido',
    CANCEL: 'Cancelar Pedido',
    SEARCHING: '🔍 Buscando Productos',
    CREATED: '🆕 Creado',
    PREPAIRING: '🧑‍🍳 Preparando',
    RECEPTION: '🏢 Recibido',
    DELIVERY: '🛵 En Reparto',
    CLIENT_DOOR: '🚪 En la Puerta',
    DONE: '✅ Entregado',
    CANCELED: '❌ Cancelado',
    ABOUT_US: 'Sobre Nosotros',
    CONTACT: 'Contacto',
    RIGHTS: 'Todos los derechos reservados.',
    DESCRIPTION: 'La mejor comida de la ciudad, entregada directamente en tu puerta.',
    WELCOME: '¡Bienvenido de nuevo!',
    REGISTER_SUBTITLE: 'Crea una cuenta para comenzar a pedir.',
    SAVE: 'Guardar',
    SESSION_END: 'La sesión finaliza en:',
    FULL_NAME: 'Nombre Completo',
    MAIL: 'Correo',
    FULL_ADDRESS: 'Dirección Completa',
    ROLE: 'Rol',
    THEME_LIGHT: 'Claro',
    THEME_DARK: 'Oscuro',
    ITEMS: 'Productos',
    EMPTY_CART: 'Tu carrito está vacío. ¡Agrega productos desde el menú!',
    TOTAL: 'Total',
    ALREADY_HAVE_ACCOUNT: '¿Ya tienes una cuenta?',
  },
  en: {
    ORDER_STATUS: 'Order Status',
    CURRENT_ORDER: 'Current Order',
    CART: 'Cart',
    PROFILE: 'Profile',
    MY_PROFILE: 'My Profile',
    LOGOUT: 'Logout',
    EMAIL: 'Email Address',
    PASSWORD: 'Password',
    CREATE_ACCOUNT: 'Create Account',
    LOGIN: 'Login',
    REGISTER: 'Register',
    NAME: 'First Name',
    SURNAME: 'Last Name',
    CODEAREA: 'Area Code',
    PHONE: 'Phone Number',
    STREET: 'Street',
    NUMBER: 'Number',
    SELECT: 'Select',
    DEPARTAMENT: 'Department',
    LOCALITY: 'Locality',
    RE_PASSWORD: 'Repeat Password',
    REQUEST: 'Place Order',
    CANCEL: 'Cancel Order',
    SEARCHING: '🔍 Searching Products',
    CREATED: '🆕 Created',
    PREPAIRING: '🧑‍🍳 Preparing',
    RECEPTION: '🏢 Received',
    DELIVERY: '🛵 Delivering',
    CLIENT_DOOR: '🚪 At Your Door',
    DONE: '✅ Delivered',
    CANCELED: '❌ Cancelled',
    ABOUT_US: 'About Us',
    CONTACT: 'Contact',
    RIGHTS: 'All rights reserved.',
    DESCRIPTION: 'The best food in town, delivered right to your door.',
    WELCOME: 'Welcome back!',
    REGISTER_SUBTITLE: 'Create an account to start ordering.',
    SAVE: 'Save Changes',
    SESSION_END: 'Session ends in:',
    FULL_NAME: 'Full Name',
    MAIL: 'Email',
    FULL_ADDRESS: 'Full Address',
    ROLE: 'Role',
    THEME_LIGHT: 'Light',
    THEME_DARK: 'Dark',
    ITEMS: 'Items',
    EMPTY_CART: 'Your cart is empty. Add products from the menu!',
    TOTAL: 'Total',
    ALREADY_HAVE_ACCOUNT: 'Already have an account?',
  }
};
