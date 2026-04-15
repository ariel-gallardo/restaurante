export default {
    templateUrl: '/components/views/OrderItemComponent.html5',
    controllerAs: 'ctrl',
    controller: 'OrderItemCtrl',
    bindings: { 
        onPlus: '=',
        onMinus: '=',
        onRemove: '=',
        infoId: '<',
        infoQuantity: '<' 
    }
}