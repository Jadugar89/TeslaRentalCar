import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthService } from './auth.service';
import { AuthApiService } from './auth-api.service';
import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [
    LoginComponent
  ],
  providers:[AuthService,AuthApiService],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
})
export class AuthenticationModule { }
