import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/services/Login';
import { Token } from 'src/services/Token';
import { UserService } from 'src/services/UserService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent
  implements OnInit {

  constructor(
    private service: UserService,
    private route: Router
    ) { }
  ngOnInit(): void {
    sessionStorage.clear()
  }

  hide : boolean = true

  login: Login =
  {
    login: '',
    password: ''
  }

  errorMessage : string = ''
  error = false

  btnLogin = () => {
    this.service.getToken(this.login)
      .subscribe({
        next: (res: Token) => {
          console.log(res);

          sessionStorage.setItem('jwtAuthenticator', res.token);

          this.route.navigate(["/"])
        },
        error: (err: HttpErrorResponse) => {
          this.error = true;
          switch (err.status) {
            case 404:
              this.errorMessage = 'Incorrect username or pass'
              break
            
            default:
              this.errorMessage = 'Error: ' + err.status
              break
          }
        }
      })
  }
}
