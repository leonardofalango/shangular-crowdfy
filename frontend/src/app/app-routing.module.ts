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
/* FROM ANGULAR MATERIAL */


const routes: Routes = [
  {path: "", component: MainComponent}
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
    MatListModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
}
