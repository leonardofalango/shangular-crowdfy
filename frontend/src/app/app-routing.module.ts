import { NgModule } from '@angular/core';
import { MainComponent } from './main/main.component';
import { RouterModule, Routes } from '@angular/router';

/* FROM ANGULAR MATERIAL */
import { MatDialogModule } from '@angular/material/dialog';
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatBadgeModule } from '@angular/material/badge';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { SignInComponent } from './sign-in/sign-in.component';
import { LoginComponent } from './login/login.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule } from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SearchPageComponent } from './search-page/search-page.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material/card';
/* FROM ANGULAR MATERIAL */

const routes: Routes = [
  {path: "", component: MainComponent},
  {path: "login", component: LoginComponent},
  {path: "signup", component: SignInComponent},
  {path: "search/:searchText", component: SearchPageComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes),
    MatDialogModule,
    MatStepperModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatBadgeModule,
    MatInputModule,
    MatListModule,
    MatCheckboxModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatNativeDateModule,
    MatDatepickerModule,
    NgxSpinnerModule,
    MatSlideToggleModule,
    MatCardModule,
    MatSelectModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
}
