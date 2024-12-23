export default class NavBarController {

    constructor($scope, $location) {
        this.$scope = $scope;
        this.$location = $location;
        this.verCarrito = this.verCarrito.bind(this);
        this.perfilUsuario = this.perfilUsuario.bind(this)
    }

    verCarrito() {
        this.$location.path('/cart');
    }

    perfilUsuario(){
        this.$location.path('/profile');
    }
}