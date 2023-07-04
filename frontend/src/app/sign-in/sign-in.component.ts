import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/services/User';
import { UserService } from 'src/services/UserService';



class CustomValidator {
  static MatchValidator(source: string, target: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const sourceCtrl = control.get(source)
      const targetCtrl = control.get(target)

      return sourceCtrl && targetCtrl && sourceCtrl.value !== targetCtrl.value
        ? { mismatch: true }
        : null
    }
  }
}

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
  secondFormGroup = this._formBuilder.group({
    secondCtrl: ['', Validators.required],
  });
  isLinear = false;

  hide: boolean = false

  
  name: string = ''
  lastname: string = ''
  username: string = ''
  mail: string = ''
  borndate: string = ''
  photo: string = ''
  password: string = ''

  sendUser() {
    const user: User = 
    {
      id: 0,
      completeName: this.name! + this.lastname!,
      username: this.username!,
      photo: this.photo!,
      bornDate: new Date(this.borndate),
      mail: this.mail,
      isAuth: 0
    }

    console.log(user);
    

    this.service.createUser(user).subscribe(
      res => {
        console.log(res)
        this.router.navigate(['login'])
      }
    )
  }
  
  
  //! FORM CONTROL
  constructor(
    private _formBuilder: FormBuilder,
    private service: UserService,
    private router: Router
    ) {  }
  get passwordMatchError() {
    return (
      this.secondFormGroup.getError('mismatch') &&
      this.secondFormGroup.get('confirmPassword')?.touched
    );
  }

  profileForm = new FormGroup(
    {
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required]),
    },
    [CustomValidator.MatchValidator('password', 'confirmPassword')]
  );
}
