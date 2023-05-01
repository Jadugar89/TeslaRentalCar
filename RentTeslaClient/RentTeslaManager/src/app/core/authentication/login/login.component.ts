import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { ILogin } from '../models/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl:'./login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm = this.formBuilder.group({
    email: ['', [Validators.required,Validators.email]],
    password: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder,
              private authService:AuthService,
              private router: Router) {
    
   }


  onSubmit() {
    if (this.loginForm.valid) {
      // do login logic here
      this.authService.login(this.loginForm.value as ILogin).subscribe(() => {
        this.router.navigate(['/cars']);
      });

    }
  }
}
