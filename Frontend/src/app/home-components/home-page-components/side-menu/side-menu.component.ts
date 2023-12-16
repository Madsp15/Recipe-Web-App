import { Component, OnInit } from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {TokenService} from "../../../../services/token.service";

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


  constructor(private router : Router, private token: TokenService,
              private toast: ToastController) {}

  ngOnInit() {
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

  clickDraft() {

  }
}
