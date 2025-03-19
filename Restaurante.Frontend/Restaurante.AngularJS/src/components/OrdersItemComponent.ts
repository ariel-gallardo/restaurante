export default {
    templateUrl: '/components/views/OrdersItemComponent.html5',
    controllerAs: 'ctrl',
    controller: 'OrdersItemCtrl',
    bindings: { 
        order: '<',
        onViewMap: '=',
        onViewDetails: '=',
        onModifyDetails: '=',
        onCancel: '=',
        permissions: '<'
    }
}