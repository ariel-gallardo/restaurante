import Ingredient from "@models/Ingredient/Ingredient"

export default interface Product{
    id: string
    nombre: string
    descripcion: string
    imagenUrl: string
    ingredientes: Array<Ingredient>
    precioCompra: number
    precioVenta: number
    stockActual: number
    stockAlerta: number
    unidad: string
    createdAt: string
    deletedAt: string
    updatedAt: string
}