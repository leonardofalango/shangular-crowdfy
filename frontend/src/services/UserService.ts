import { Injectable } from '@angular/core';
import { User } from './User';
import { HttpClient } from '@angular/common/http'

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
        return this.http.get<User>("http://localhost:5177/user/" + id)
    }

    validate(jwt: string | null)
    {
        return this.http.post<User>("http://localhost:5177/user/validate", jwt)
    }

    createUser(user: User)
    {
        return this.http.post<string>("http://localhost:5177/user/add", user)
    }
}