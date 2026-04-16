export default {
    templateUrl: '/components/views/OrderListComponent.html5',
    controllerAs: 'ctrl',
    controller: 'OrderListCtrl',
    bindings: { 
        data: '<',
        onPlus: '=',
        onMinus: '=',
        onRemove: '='
    }
}

