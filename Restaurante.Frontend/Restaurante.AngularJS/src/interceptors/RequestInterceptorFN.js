import ResponseServices from "@services/ResponseServices";
import EnvironmentServices from "@services/EnvironmentServices";
import { RestauranteServices } from "@services/RestauranteServices";
/**
 * Interceptor para agregar el token de autenticación desde las cookies y manejar errores de respuesta.
 * 
 * @param {angular.IQService} $q - El servicio `$q` para manejar promesas en AngularJS.
 * @param {angular.cookies.ICookiesService} $cookies - El servicio `$cookies` para acceder a las cookies.
 * @param {angular.ILocationService} $location - El servicio `$location` para acceder a las rutas.
 * @param {ResponseServices} ResponseServices - El servicio `$location` para acceder a las rutas.
 * @param {angular.IRootScopeService} ResponseServices - El servicio ResponseServices.
 * @param {EnvironmentServices} EnvironmentServices - El servicio EnvironmentServices.
 * @returns {Object} - El interceptor con métodos para `request`, `response` y `responseError`.
 */
const RequestInterceptorFN = ($q, $cookies, $location, ResponseServices, $rootScope, EnvironmentServices) => {
    return {
        /**
         * Método que agrega el token de autenticación a las cabeceras de la solicitud.
         * 
         * @param {Object} config - La configuración de la solicitud HTTP.
         * @returns {Object} - La configuración de la solicitud modificada con el token de autenticación si está presente.
         */
        request: (config) => {
            let API_ADDRESS = EnvironmentServices.ApiAdress;
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
            }else if(config.url.includes('/components/views/')){
                let cssFileName = config.url.substring(config.url.lastIndexOf('/')+1).replace('.html5','.css');
                let head = document.getElementsByTagName('head')[0];
                if(!document.getElementById(cssFileName)){
                    let link = document.createElement('link');
                    link.id = cssFileName;
                    link.rel = 'stylesheet';
                    link.href = `/assets/styles/components/${cssFileName}`;
                    head.appendChild(link);
                }
            }else if(config.url.includes('/views/')){
                let cssFileName = config.url.substring(config.url.lastIndexOf('/')+1).replace('.html5','.css');
                let head = document.getElementsByTagName('head')[0];
                if(!document.getElementById(cssFileName)){
                    let link = document.createElement('link');
                    link.id = cssFileName;
                    link.rel = 'stylesheet';
                    link.href = `/assets/styles/views/${cssFileName}`;
                    head.appendChild(link);
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
            let API_ADDRESS = EnvironmentServices.ApiAdress;
            if(response.config.url.includes(API_ADDRESS)){
                if(response.data && response.data.content){
                    ResponseServices.newData(response.data);
                    $rootScope.$emit('showResponseToast'); 
                    if(response.data.content.token){
                        $cookies.put('auth_token', response.data.content.token);
                        response.data.content = {...response.data.content,token:null}
                        localStorage.setItem('userInfo', JSON.stringify(response.data.content));
                    }  
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
                console.log(rejection.data)
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