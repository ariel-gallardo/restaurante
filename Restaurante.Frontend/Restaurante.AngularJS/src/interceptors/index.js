import RequestInterceptorFN from "./RequestInterceptorFN";

export default {
    RequestInterceptor: ['$q','$cookies', '$location', 'ResponseServices', '$rootScope',RequestInterceptorFN]
}