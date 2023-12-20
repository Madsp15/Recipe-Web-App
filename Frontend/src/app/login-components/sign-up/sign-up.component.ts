import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {HttpClient, HttpClientModule, HttpErrorResponse} from "@angular/common/http";
import {firstValueFrom} from "rxjs";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [CommonModule, IonicModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  usernameInput = new FormControl('', Validators.required);
  emailInput = new FormControl('', Validators.required);
  passwordInput = new FormControl('', Validators.required);

  formGroup = new FormGroup({
    username: this.usernameInput,
    email: this.emailInput,
    password: this.passwordInput,
  });

  showPassword: boolean = false;
  constructor(private router: Router,
              public toastController: ToastController,
              public  http: HttpClient){}

  async clickJoin() {
    try {
      const result = this.http.post(environment.baseUrl +'/api/users', this.formGroup.getRawValue());
      const response = await firstValueFrom(result);
      const toast = await this.toastController.create({
        message: 'Successfully created an account'
      });
      toast.duration = 5000;
      toast.present();
      this.router.navigate(['login'], { replaceUrl: true });

    } catch (e) {
      if (e instanceof HttpErrorResponse) {
        const errorResponse = e.error;
        const allErrorMessages = Object.values(errorResponse.errors).flat();
        console.log(e.message);
        console.log(allErrorMessages);

        const toast = await this.toastController.create({
          message: allErrorMessages[0]?.toString() || 'An error occurred.'
        });
        toast.duration = 5000;
        toast.present();
      }
    }
  }

  clickBack() {
    this.router.navigate(['login'] , {replaceUrl: true});
  }
}
