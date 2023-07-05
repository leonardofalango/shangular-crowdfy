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

    authenticate = (tokenStr: string | null) =>
    {
        console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
        console.log({token: tokenStr})
        this.http.post<User>("localhost:5177/user/validateToken", {token: tokenStr})
            .subscribe(e=> console.log(e))
    }
}




// .subscribe({
//     next: (res : User) => {
//         this.user = res
//         return this.user
//     },
//     error: (err : HttpErrorResponse) => {
//         console.error(err.message)

//         switch(err.status) {
//             case 404:
//                 console.error(err.error);
//                 alert("User Not Found")
//                 break
//             case 401:
//                 console.error(err.error);
//                 alert('Not authorized user')
//                 break
//             case 500:
//                 console.error(err.error)
//                 alert('Server Error: ' + err.message)
//                 break
            
//             default:
//                 console.error(err.error)
//                 alert('Internal Error: ' + err.message)
//                 break                                              
//         }
//         this.router.navigate(["login"])
//     }
// })
