import { Component, OnInit } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {LoginComponent} from "../login/login.component";
import {Router, RouterOutlet} from "@angular/router";

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

  constructor(private router : Router) { }

  ngOnInit() {}

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  clickHome() {
    this.router.navigate([''], {replaceUrl:true})
  }
  isRouteActive(route: string): boolean {
    return this.router.isActive(route, true);
  }

  clickSignUp() {

  }

  clickForgotPassword() {

  }
}
