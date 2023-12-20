import { Component } from '@angular/core';
import {AlertController, IonicModule} from "@ionic/angular";
import {AccountService} from "../../../services/account.service";
import {User} from "../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {TokenService} from "../../../services/token.service";

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

  usernameInput: FormControl<string | null> = new FormControl('', Validators.required);
  emailInput = new FormControl('', Validators.required);


  formGroup = new FormGroup({
    username: this.usernameInput,
    email: this.emailInput,
  });

  constructor(public accountService: AccountService,
              private http: HttpClient,
              private router: Router,
              private token: TokenService,
              private  alertController :AlertController) {
    this.autofill();
  }


  async clickDeleteUser() {
    const alert = await this.alertController.create({
      header: 'Delete Account',
      message: 'Are you sure you want to delete your account? All your recipes will be deleted and changes cannot be reverted.',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => {
            console.log('Cancel clicked');
          }
        },
        {
          text: 'Yes',
          handler: async () => {
            try {
              const account: User = await firstValueFrom(this.accountService.getCurrentUser());
              const call = this.http.delete<boolean>('http://localhost:5280/api/users/' + account.userId);
              const result = await firstValueFrom<boolean>(call);
              this.token.clearToken();
              await this.router.navigate(['/login'], { replaceUrl: true });

            } catch (error) {
              console.error('Error during account deletion:', error);
            }
          }
        }
      ]
    });
    await alert.present();
  }

  async autofill() {
    let account: User = await firstValueFrom(this.accountService.getCurrentUser());
    this.formGroup.patchValue(
      {
        username: account.userName,
        email: account.email,
      }
    )
  }

  async clickSaveChanges() {
    let account: User = await firstValueFrom(this.accountService.getCurrentUser());
    if (account.email != this.formGroup.getRawValue().email) {
      const call = this.http.put<User>('http://localhost:5280/api/account/email/' + account.userId, {email: this.formGroup.getRawValue().email},);
      const result = await firstValueFrom<User>(call)
      console.log(result);
    }
    if (account.userName != this.formGroup.getRawValue().username) {
      const call = this.http.put<User>('http://localhost:5280/api/account/Username/' + account.userId, {userName: this.formGroup.getRawValue().username},);
      const result = await firstValueFrom<User>(call)
      console.log(result);
    }
  }

  async clickCancel() {
    await this.router.navigate(['/home'], {replaceUrl: true});
  }
}
