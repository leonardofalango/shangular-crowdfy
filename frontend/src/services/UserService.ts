import { Injectable } from '@angular/core';
import { User, UserPassword } from './User';
import { HttpClient } from '@angular/common/http'
import { Login } from './Login';
import { Token } from './Token';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private http: HttpClient) { }

    getAllUsers()
    {
        return this.http.get<User[]>("http://localhost:5177/user/");
    }

    getUser(id: string | null)
    {
        return this.http.get<User>("http://localhost:5177/user/getById/" + id)
    }

    createUser(user: UserPassword)
    {
        return this.http.post<string>("http://localhost:5177/user/add", user)
    }

    getToken(login: Login)
    {
        return this.http.post<Token>("http://localhost:5177/user/login", login)
    }
}