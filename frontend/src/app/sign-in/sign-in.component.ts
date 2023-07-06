import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
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

  backButton = () => {
    this.router.navigate(['/login'])
  }

  hide: boolean = true
  name: string = ''
  lastName: string = ''
  password: string = ''
  repPass: string = ''
  noMatchError: boolean = false
  dateError: boolean = false;
  noLenghtError: boolean = false;

  nameError = false
  lastnameError = false
  usernameError = false
  mailError = false

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

  disabledFirstGroup:boolean = true
  firstGroupCheck = () => {
    this.mailError = !(this.user.mail.includes("@") && this.user.mail.includes("."))
    this.nameError = this.name == ''
    this.lastnameError = this.lastName == ''
    this.usernameError = this.user.username == ''

    this.disabledFirstGroup = (
      this.nameError ||
      this.lastnameError || 
      this.usernameError ||
      this.mailError
    )
  }

  disabledSecondGroup:boolean = true
  secundGroupCheck = () => {
    this.noMatchError = !(this.password == this.repPass)
    this.noLenghtError = !(this.password.length >= 8)
    this.disabledSecondGroup = !(this.password == this.repPass) || (this.password == '' || this.repPass == '') && !(this.disabledFirstGroup) || this.noLenghtError
  }

  disabledThirdGroup:boolean = true
  thirdGroupCheck = () => {
    this.firstGroupCheck()
    this.secundGroupCheck()

    
    this.disabledThirdGroup = ((new Date().getFullYear() - this.user.bornDate.getFullYear()) <= 18) || (this.disabledFirstGroup) || (this.disabledSecondGroup)
    this.dateError = new Date().getFullYear() - this.user.bornDate.getFullYear() <= 18
    console.log(
      this.disabledFirstGroup + ' ' + 
      this.disabledSecondGroup
    )
  }

  checkPass()
  {
    this.password == this.repPass?
      this.noMatchError=true :
      this.noMatchError=false
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
