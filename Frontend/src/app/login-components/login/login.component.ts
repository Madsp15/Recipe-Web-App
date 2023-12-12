import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {TokenService} from "../../../services/token.service";
import {firstValueFrom} from "rxjs";
import {AccountService, Credentials} from "../../../services/account service";
import {HttpClientModule} from "@angular/common/http";


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, HttpClientModule, IonicModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  showPassword: boolean = false;

  email = new FormControl('', Validators.required);
  password = new FormControl('', Validators.required);

  form = new FormGroup({
    email: this.email,
    password: this.password
  });

  constructor(
    private router: Router,
    private token: TokenService,
    private account: AccountService,
    private toast: ToastController
  ){}

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  async clickLogin() {
    if (this.form.invalid) return;

      const {token} = await firstValueFrom(this.account.login(this.form.value as Credentials));
      this.token.setToken(token);

      this.router.navigate(['http://localhost:4200/home/profile'], {replaceUrl:true});

      await (await this.toast.create({
        message: "Welcome",
        color: "success",
        duration: 5000
      })).present();

  }

  clickSignUp() {
    this.router.navigate(['login/sign-up'], {replaceUrl:true});
    console.log('Sign Up clicked');
  }

  clickForgotPassword() {
    this.router.navigate(['login/forgot-password'], {replaceUrl:true});
    console.log('Forgot Password clicked');
  }

}
