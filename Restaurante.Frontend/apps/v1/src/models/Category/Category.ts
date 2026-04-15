import Pagination from "@models/Pagination"

export default class Category{
    id: number
    categoriaPadre: Category
    categoriaPadreId: number
    descripcion: string
    nombre: string
    subCategories: Pagination<Category> = new Pagination<Category>;
    hasChildren?: boolean
    selected: boolean = false;
}