import { LoginDTO } from "./login-return"

export interface ForumData
{
    titulo: string, 
    descricao: string,
    dataCriacao: Date,
    jwt: string,
    quantidade: number
}