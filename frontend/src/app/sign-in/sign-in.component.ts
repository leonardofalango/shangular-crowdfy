import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { HttpErrorResponse } from '@angular/common/http';
import { identifierName } from '@angular/compiler';
import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserPassword } from 'src/services/User';
import { UserService } from 'src/services/UserService';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: {showError: true}
    }
  ]
})
export class SignInComponent {

  firstFormGroup = this._formBuilder.group({
    firstCtrl: ['', Validators.required],
  });
  secondFormGroup = this._formBuilder.group(
  {}
    
  );
  isLinear = false;

  hide: boolean = true
  name: string = ''
  lastName: string = ''
  password: string = ''

  user : UserPassword =
  {
    id: 0,
    completeName: '',
    username: '',
    photo: '',
    bornDate: new Date(),
    mail: '',
    isAuth: 0,
    password: ''
  }

  sendUser() {
    this.user.completeName = this.name + ' ' + this.lastName
    this.user.password = this.password

    console.log(this.user);
    

    this.service.createUser(this.user).subscribe({
      next: (res : string) => {
        console.log(res)
        alert('Success')
        this.router.navigate(['login'])
      },
      error: (err: HttpErrorResponse) => {
        alert(err.message)
        console.error(err)
      }
    })
  }
  
  
  //! FORM CONTROL
  constructor(
    private _formBuilder: FormBuilder,
    private service: UserService,
    private router: Router
    ) {  }
}
