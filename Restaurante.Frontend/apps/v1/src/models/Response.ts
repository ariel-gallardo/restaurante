export default interface Response<T>{
    content?: T
    message: string
    statusCode: number
}


export interface GenericResponse{
    message: string
    statusCode: number
}