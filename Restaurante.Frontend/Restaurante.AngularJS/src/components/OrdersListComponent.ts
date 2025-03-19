export default {
    templateUrl: '/components/views/OrdersListComponent.html5',
    controllerAs: 'ctrl',
    controller: 'OrdersListCtrl',
    bindings: { 
        data: '<',
        onViewMap: '=',
        onViewDetails: '=',
        onModifyDetails: '=',
        onCancel: '=',
        permissions: '<'
    }
}