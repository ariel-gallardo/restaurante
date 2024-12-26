/**
 * Interceptor para agregar el token de autenticación desde las cookies y manejar errores de respuesta.
 * 
 * @param {angular.$q} $q - El servicio `$q` para manejar promesas en AngularJS.
 * @param {angular.$cookies} $cookies - El servicio `$cookies` para acceder a las cookies.
 * @param {angular.$location} $location - El servicio `$location` para acceder a las rutas.
 * @returns {Object} - El interceptor con métodos para `request`, `response` y `responseError`.
 */
const RequestInterceptorFN = ($q, $cookies, $location, ResponseServices, $rootScope) => {
    return {
        /**
         * Método que agrega el token de autenticación a las cabeceras de la solicitud.
         * 
         * @param {Object} config - La configuración de la solicitud HTTP.
         * @returns {Object} - La configuración de la solicitud modificada con el token de autenticación si está presente.
         */
        request: (config) => {
            let API_ADDRESS = process.env.API_ADDRESS;
            if(config.url.startsWith('/api')){
                config.headers.Accept = '*/*';
                config.headers['Content-Type'] = 'application/json';
                config.url = `${API_ADDRESS}${config.url}`;
            }
            if(config.url.includes(API_ADDRESS)) {
                let token = $cookies.get('auth_token');
                if (token) {
                    config.headers['Authorization'] = token;
                }
            }
            return config;
        },

        /**
         * Método que maneja la respuesta exitosa de la solicitud.
         * 
         * @param {Object} response - La respuesta HTTP de la solicitud.
         * @returns {Object} - La respuesta tal como fue recibida.
         */
        response: (response) => {
            let API_ADDRESS = process.env.API_ADDRESS;
            if(response.config.url.includes(API_ADDRESS)){
                if(response.data && response.data.content){
                    ResponseServices.newData(response.data);
                    $rootScope.$emit('showResponseToast');
                    if(response.data.content.token){
                        $cookies.put('auth_token', response.data.content.token);
                        response.data.content = {...response.data.content,token:null}
                        localStorage.setItem('userInfo', JSON.stringify(response.data.content));
                    }
                    $location.path('/home');   
                }
            }
            return response;
        },

        /**
         * Método que maneja los errores de respuesta HTTP.
         * @param {Object} rejection - El objeto de rechazo de la respuesta (que contiene información del error).
         * @returns {Promise} - Retorna la promesa rechazada con el error.
         */
        responseError: (rejection) => {
            if(rejection.data){
                ResponseServices.newData(rejection.data);
                if(rejection.status >= 400){
                    rejection.data = null;
                    if(rejection.status == 401)
                    {
                        try{$cookies.remove('auth_token');}catch(e){}
                        $location.path('/login')
                    }
                }
                $rootScope.$emit('showResponseToast');
            } 
            return $q.reject(rejection);
        }
    }
};


export default RequestInterceptorFN;