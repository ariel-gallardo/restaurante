import IUserInfo from "@models/User/IUserInfo";
import { IFilterService, IWindowService } from "angular";

export default class LocalStorageServices{

    static $inject = ['$filter','$window'];
    private LS : Storage;
    private ExpirationTimeFilter: (dateString) => '';

    public get UserInfo(){
        let uD = JSON.parse(this.LS.getItem('userInfo'));
        let u = {} as IUserInfo;
        u.NombreCompleto = uD?.nombreCompleto ?? '-';
        u.Correo = uD?.correo ?? '-';
        u.Rol = uD?.tipoDeUsuario ?? '-';
        u.Domicilio = uD?.domicilio ?? '-';
        u.Telefono = uD?.telefono ?? '-';
        u.ImagenUrl = uD?.imagenUrl ?? '/assets/images/commonUser.svg';
        u.TiempoExpiracionToken = uD?.caducaEn ? this.ExpirationTimeFilter(uD.caducaEn) : '-';
        return u;
    };

    RemoveUserInfo(){
        this.LS.removeItem('userInfo');
    }

    constructor($filter: IFilterService, $window: IWindowService) {
        this.LS = $window.localStorage;
        this.ExpirationTimeFilter = $filter('ExpirationTime');
    }
}