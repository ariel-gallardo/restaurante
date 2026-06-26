import EnvironmentServices from "@services/EnvironmentServices";
import MessageServices from "@services/MessageServices";
/**
 * Interceptor para agregar el token de autenticación desde las cookies y manejar errores de respuesta.
 * 
 * @param {angular.IQService} $q - El servicio `$q` para manejar promesas en AngularJS.
 * @param {angular.cookies.ICookiesService} $cookies - El servicio `$cookies` para acceder a las cookies.
 * @param {angular.ILocationService} $location - El servicio `$location` para acceder a las rutas.
 * @param {EnvironmentServices} EnvironmentServices - El servicio EnvironmentServices.
 * @param {MessageServices} MessageServices - El servicio EnvironmentServices.
 * @returns {Object} - El interceptor con métodos para `request`, `response` y `responseError`.
 */
const RequestInterceptorFN = ($q, $cookies, $location, $rootScope, EnvironmentServices, MessageServices) => {
    const toAbsolutePublicUrl = (relativePath) => {
        if (!relativePath) return relativePath;
        const publicBase = EnvironmentServices.LegacyPublicBaseUrl;
        if (relativePath.startsWith('http://') || relativePath.startsWith('https://') || relativePath.startsWith(publicBase)) {
            return relativePath;
        }
        return `${publicBase}${relativePath}`;
    };

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
                if(MessageServices.ConnectionId) config.headers['GroupId'] = MessageServices.ConnectionId;
                config.url = `${API_ADDRESS}${config.url}`;
            }
            if(config.url.includes(API_ADDRESS)) {
                let token = $cookies.get('auth_token');
                if (token) {
                    config.headers['Authorization'] = token;
                }
            }else if(config.url.includes('/components/views/')){
                config.url = toAbsolutePublicUrl(config.url);
                let cssFileName = config.url.substring(config.url.lastIndexOf('/')+1).replace('.html5','.css');
                let head = document.getElementsByTagName('head')[0];
                if(!document.getElementById(cssFileName)){
                    let link = document.createElement('link');
                    link.id = cssFileName;
                    link.rel = 'stylesheet';
                    link.href = toAbsolutePublicUrl(`/assets/styles/components/${cssFileName}`);
                    head.appendChild(link);
                }
            }else if(config.url.includes('/views/')){
                config.url = toAbsolutePublicUrl(config.url);
                let cssFileName = config.url.substring(config.url.lastIndexOf('/')+1).replace('.html5','.css');
                let head = document.getElementsByTagName('head')[0];
                if(!document.getElementById(cssFileName)){
                    let link = document.createElement('link');
                    link.id = cssFileName;
                    link.rel = 'stylesheet';
                    link.href = toAbsolutePublicUrl(`/assets/styles/views/${cssFileName}`);
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
                    if(response.data.content.token){
                        $cookies.put('auth_token', response.data.content.token);
                        const userInfo = {...response.data.content, token: null};
                        localStorage.setItem('userInfo', JSON.stringify(userInfo));
                        if (window.angularStore) {
                            window.angularStore.dispatch({
                                type: 'AUTH/SET_USER',
                                payload: userInfo
                            });
                        }
                    }  
                    $rootScope.$emit('CheckNextUrl');
                }
            } else if (
                typeof response.data === 'string' &&
                (response.config.url.includes('/views/') || response.config.url.includes('/components/views/'))
            ) {
                const publicBase = EnvironmentServices.LegacyPublicBaseUrl;
                response.data = response.data
                    .replace(/(src|href)="\/assets\//g, `$1="${publicBase}/assets/`)
                    .replace(/url\('\/assets\//g, `url('${publicBase}/assets/`)
                    .replace(/url\("\/assets\//g, `url("${publicBase}/assets/`);
            }
            return response;
        },

        /**
         * Método que maneja los errores de respuesta HTTP.
         * @param {Object} rejection - El objeto de rechazo de la respuesta (que contiene información del error).
         * @returns {Promise} - Retorna la promesa rechazada con el error.
         */
        responseError: (rejection) => {
            $rootScope.$emit('RemoveNextUrl');
            if(rejection.data){
                if(rejection.status >= 400){
                    rejection.data = null;
                    if(rejection.status == 401)
                    {
                        try{$cookies.remove('auth_token');}catch(e){}
                        $location.path('/login')
                    }
                }
            } 
            return $q.reject(rejection);
        }
    }
};


export default RequestInterceptorFN;