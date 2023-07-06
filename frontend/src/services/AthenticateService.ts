import { Injectable } from '@angular/core';
import { User } from './User';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
  })
export class AthenticateService {

    constructor(
        private http: HttpClient,
        private router: Router
    ) { }

    user: User =
    {
        id: 0,
        completeName: '',
        username: '',
        photo: '',
        bornDate: new Date(),
        mail: '',
        isAuth: 0
    }

    authenticate = (tokenStr: string | null) =>
    {
        this.http.post<User>("http://localhost:5177/user/validateToken", {token: tokenStr})
        .subscribe({
            next: (res : User) => {
                this.user = res

                sessionStorage.setItem("userId", res.id.toString())

                return this.user
            },
            error: (err : HttpErrorResponse) => {
                console.error(err.message)
        
                switch(err.status) {
                    case 404:
                        console.error(err.error);
                        alert("User Not Found")
                        break
                    case 401:
                        console.error(err.error);
                        alert('Not authorized user')
                        break
                    case 500:
                        console.error(err.error)
                        alert('Server Error: ' + err.message)
                        break
                    
                    default:
                        console.error(err.error)
                        alert('Internal Error: ' + err.message)
                        break                                              
                }
                this.router.navigate(["login"])
            }
        })
        
    }
}




