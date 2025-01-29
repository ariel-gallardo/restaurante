export default class Pagination<T>{
    cantidadActual: number = 0;
    content: T[] = [];
    paginaActual: number = 0;
    paginaAnterior: number = 0;
    paginaSiguiente: number = 0;
    paginasTotales: number = 0;
    total: number = 0;
}