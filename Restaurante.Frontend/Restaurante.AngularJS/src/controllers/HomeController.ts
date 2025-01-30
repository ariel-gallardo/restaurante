import Category from "@models/Category/Category";
import Pagination from "@models/Pagination";
import Product from "@models/Product/Product";
import ProductGetQuerie from "@queries/ProductGetQuerie";
import ProductStoreMin from "@models/Product/ProductStoreMin";
import CategoryServices from "@services/CategoryServices";
import ProductoServices from "@services/ProductoServices";
import angular, { forEach, IRootScopeService, IScope } from "angular";
import CategoryGetQuerie from "@queries/CategoryGetQuerie";

export default class HomeController{

    private static $inject = ['$rootScope', '$scope', 'ProductoServices', 'CategoryServices'];
    private _querie: ProductGetQuerie = new ProductGetQuerie;
    private _currentProducts: Pagination<Product> = new Pagination<Product>;
    private _currentCategories: Pagination<Category> = new Pagination<Category>; 
    private _categories : Category[] = [];
    private _products : ProductStoreMin[] = [];
    private _catQuerie: CategoryGetQuerie = new CategoryGetQuerie;
    private _currentCat: Category;
    private _lastCat: Category;


    public get ProductsStore(): ProductStoreMin[]{
        return this._products;
    }

    public get CategoriesStore(): Category[]{
        return this._categories;
    }

    public async SeleccionarCategoria(cat: Category, evt: PointerEvent | any){
        evt.stopPropagation();
        let jQElement = $(`#cat-${cat.id}`);

        this._querie.categoria = cat.id;
        this._lastCat = this._lastCat?.id != cat.id && this._currentCat ? this._currentCat : null;
        this._currentCat = cat;

        if(this._lastCat) this._lastCat.selected = !this._lastCat.selected;
        this._currentCat.selected = true;
        this._catQuerie.catPadreId = this._currentCat.id;

        if(this._currentCat.hasChildren == null){
            this._currentCat.subCategories = await this.CategoryServices.getAll(this._catQuerie);
            this._currentCat.hasChildren = this._currentCat.subCategories.total > 0;
            if(this._currentCat.hasChildren)
                this._currentCat.subCategories.content = this._currentCat.subCategories.content.map(x => {
                x.categoriaPadre = cat;
                x.categoriaPadreId = cat.id;
                return x;
            });
        }

        if(cat.hasChildren){
            if(jQElement.children().length == 0){
                let ulList = $('<ul></ul>').addClass('list-group');
                this._currentCat.subCategories.content.map(x =>{
                    let newElement =  
                    $('<li></li>').addClass('list-group-item')
                    .attr('id',`cat-${x.id}`)
                    .text(x.nombre);
                    let ngElement = angular.element(newElement);
                    ngElement.on('click', (evt) => {
                        this.SeleccionarCategoria(x,evt)
                    });
                    ngElement.on('hidden',() => console.log('hidden'));
                    return newElement;
                }).forEach(e => {
                    ulList.append(e);
                })
                jQElement.append(ulList);
            }else{
                if(cat.selected && cat.hasChildren){
                    cat.subCategories.content.forEach((x : Category)  => {
                        $(`#cat-${x.id}`).toggleClass('not-visible');
                    });
                }
            }
        }
        this.$scope.$emit('searchProducts');
    }

    constructor(private $rootScope: IRootScopeService, private $scope: IScope, 
        private ProductoServices: ProductoServices,
        private CategoryServices: CategoryServices) {

        this.$scope.$on('searchProducts', async () => {
            this._currentProducts = await this.ProductoServices.getAll(this._querie);
            this._products = this._currentProducts.content ? this._currentProducts.content.map(x => ({
                id: x.id,
                descripcion: x.descripcion,
                imagenUrl: x.imagenUrl,
                nombre: x.nombre,
                precioVenta: x.precioVenta,
                stockActual: x.stockActual,
                unidad: x.unidad
            })) : []
        });
        
        this.$scope.$on('$viewContentLoaded', async () => {
            this._currentProducts = await this.ProductoServices.getAll(this._querie);
            this._products = this._currentProducts.content ? this._currentProducts.content.map(x => ({
                id: x.id,
                descripcion: x.descripcion,
                imagenUrl: x.imagenUrl,
                nombre: x.nombre,
                precioVenta: x.precioVenta,
                stockActual: x.stockActual,
                unidad: x.unidad
            })) : []
            this._currentCategories = await this.CategoryServices.getAll();
            this._categories = this._currentCategories.content ?? [];
        });

        this.SeleccionarCategoria = this.SeleccionarCategoria.bind(this);
    }
}