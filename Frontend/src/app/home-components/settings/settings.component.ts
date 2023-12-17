import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {AccountService} from "../../../services/account.service";
import {User} from "../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    IonicModule,
    ReactiveFormsModule
  ],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent {

  usernameInput = new FormControl('', Validators.required);
  emailInput = new FormControl('', Validators.required);
  passwordInput = new FormControl('', Validators.required);



  formGroup = new FormGroup({
    username: this.usernameInput,
    email: this.emailInput,
    password: this.passwordInput
  });

  constructor(public accountService : AccountService, private http : HttpClient) {
  }


  async clickDeleteUser() {
    try{
    const account: User = await firstValueFrom(this.accountService.getCurrentUser());
    const call = this.http.put<User>('http://localhost:5280//api/users/', account.userId, );
    const result = await firstValueFrom<User>(call)
    console.log(result);}
    catch(error){}
  }

  async clickSaveChanges() {
    const account: User = await firstValueFrom(this.accountService.getCurrentUser());

  }

  async clickCancel() {
    const account: User = await firstValueFrom(this.accountService.getCurrentUser());

  }
}
