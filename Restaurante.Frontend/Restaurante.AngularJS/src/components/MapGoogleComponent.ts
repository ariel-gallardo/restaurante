export default {
    templateUrl: '/components/views/MapGoogleComponent.html5',
    controllerAs: 'ctrl',
    controller: 'MapGoogleCtrl',
    bindings: { 
        mapId: '<',
        mapClass: '<',
        mapLatitude: '<',
        mapLongitude: '<',
        mapWidth: '<',
        mapHeight: '<',
    }
}