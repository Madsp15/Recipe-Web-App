import { Component, OnInit } from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {firstValueFrom} from "rxjs";
import {TokenService} from "../../../../services/token.service";
import {AccountService} from "../../../../services/account.service";
import {User} from "../../../models";

@Component({
  selector: 'app-side-menu',
  standalone: true,
  imports: [
    IonicModule
  ],
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss'],
})
export class SideMenuComponent  implements OnInit {
  Username: string="";
  UserPicture: string | null="";


  constructor(private router : Router, private token: TokenService,
              private toast: ToastController, private account: AccountService ) {}

  async ngOnInit() {
    try {
      var account:User = await firstValueFrom(this.account.getCurrentUser());
      this.Username = account.userName;
      this.UserPicture = account.userAvatarUrl;
    } catch (e) {
      this.Username = "";
      this.UserPicture = "https://placebear.com/300/300";
    }


  }

  clickCreateRecipe() {
    this.router.navigate(['/home/create-recipe'], {replaceUrl:true});
  }

  clickMyRecipes() {

  }

  clickSavedRecipes() {

  }

  async clickLogOut() {
      this.token.clearToken();
      this.router.navigate(['/login'], {replaceUrl:true});
      (await this.toast.create({
        message: 'Successfully logged out',
        duration: 5000,
        color: 'success',
      })).present()
    }
}
