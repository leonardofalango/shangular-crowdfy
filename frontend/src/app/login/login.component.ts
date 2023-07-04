import { Component, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  hide = true;

  elemLog : HTMLElement | null;
  elemSign : HTMLElement | null;
  elemBody : HTMLElement | null;

  constructor(@Inject(DOCUMENT) private document: Document) {
    this.elemLog = document.querySelector('#signin')
    this.elemSign = document.querySelector('#signup')
    this.elemBody = document.querySelector('body')

  }

  displaySignIn = () => {
    this.elemBody!.className = 'sign-in-js'
  }

  displaySignUp = () => {
    this.elemBody!.className = 'sign-up-js'
  }
}
