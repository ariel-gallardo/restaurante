export interface DepartamentosDTO{
    cantidad: number;
    inicio: number;
    total: number;
    departamentos: DepartamentoDTO[];
}

export interface DepartamentoDTO{
    id: number
    nombre:string
}