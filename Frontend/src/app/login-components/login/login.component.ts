import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {Router} from "@angular/router";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, IonicModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  showPassword: boolean = false;
  password = new FormControl('', Validators.required);

  constructor(private router: Router) {
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  clickLogin() {
  }

  clickSignUp() {
    this.router.navigate(['login/sign-up']);
    console.log('Sign Up clicked');
  }

  clickForgotPassword() {
    this.router.navigate(['login/forgot-password']);
    console.log('Forgot Password clicked');
  }
}
