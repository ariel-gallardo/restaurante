export interface LocalidadesDTO{
    cantidad: number;
    inicio: number;
    total: number;
    localidades: LocalidadDTO[];
}

export interface LocalidadDTO{
    id: number
    nombre:string
    departamento: string
}