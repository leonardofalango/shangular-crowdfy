export interface User
{
    id : number
    completeName: string,
    username: string,
    photo: string,
    bornDate: Date,
    mail: string,
    isAuth: number
}

export interface UserPassword 
{
    id : number
    completeName: string,
    username: string,
    photo: string,
    bornDate: Date,
    mail: string,
    isAuth: number
    password: string
}