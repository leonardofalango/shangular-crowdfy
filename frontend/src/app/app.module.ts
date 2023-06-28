import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';
import { LogoComponent } from './logo/logo.component';
import { SearchBoxComponent } from './search-box/search-box.component';
import { ModalComponent } from './modal/modal.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainSectionComponent } from './main-section/main-section.component';
import { FormFieldComponent } from './form-field/form-field.component';
import { ProgressSpinnerComponent } from './progress-spinner/progress-spinner.component';
import { NotificationComponent } from './notification/notification.component';
import { FooterComponent } from './footer/footer.component';
import { InnerModalComponent } from './inner-modal/inner-modal.component';

// ! Angular Material
import { MatDialogModule } from '@angular/material/dialog';
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatBadgeModule } from '@angular/material/badge';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { SideBarComponent } from './side-bar/side-bar.component';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
// ! Angular Material

import { HttpClientModule } from '@angular/common/http';
import { SignInComponent } from './sign-in/sign-in.component';
import { LoginComponent } from './login/login.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    LogoComponent,
    SearchBoxComponent,
    ModalComponent,
    MainSectionComponent,
    FormFieldComponent,
    ProgressSpinnerComponent,
    NotificationComponent,
    FooterComponent,
    SideBarComponent,
    SignInComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatStepperModule,
    MatProgressSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatBadgeModule,
    InnerModalComponent,
    MatInputModule,
    MatIconModule,
    MatListModule,
    HttpClientModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
