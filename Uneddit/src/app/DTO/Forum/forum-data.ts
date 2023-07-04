import { LoginDTO } from "../Jwt/login-return"

export interface ForumData
{
    titulo: string, 
    id: number,
    descricao: string,
    dataCriacao: Date,
    jwt: string,
    quantidade: number
}