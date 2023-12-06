import { Component, OnInit } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {LoginComponent} from "../login/login.component";
import {RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    IonicModule,
    LoginComponent,
    RouterOutlet
  ],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent  implements OnInit {
  showPassword: boolean = false;

  constructor() { }

  ngOnInit() {}

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  clickLogin() {

  }

  clickSignUp() {

  }

  clickForgotPassword() {

  }
}