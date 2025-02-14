import RequestInterceptorFN from "./RequestInterceptorFN";

export default {
    RequestInterceptor: ['$q','$cookies', '$location', '$rootScope', 'EnvironmentServices', 'MessageServices',RequestInterceptorFN]
}