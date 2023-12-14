import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {UserRecipeComponent} from "../user-recipe/user-recipe.component";
import {Router} from "@angular/router";
import {TokenService} from "../../../services/token.service";
import {IonicModule, ToastController} from "@ionic/angular";import {UserRecipeComponent} from "../recipe-components/user-recipe/user-recipe.component";
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {User} from "../../models";
import {firstValueFrom} from "rxjs";
import {AccountService} from "../../../services/account service";

@Component({
  selector: 'app-recipe-profile',
  standalone: true,
  imports: [CommonModule, IonicModule, UserRecipeComponent, FormsModule, HttpClientModule],
  templateUrl: './recipe-profile.component.html',
  styleUrls: ['./recipe-profile.component.css']
})

export class RecipeProfileComponent implements OnInit {
  constructor(private router : Router,
              private toast: ToastController,
              private account: AccountService,) {}



  isEditMode = false;
  moreInfo: string = '';
  editedDescription: string = '';
  username: string = 'User';
  email: string = '';
  avatarUrl: string | null = '';
  amountOfRecipes: string = '0';



  async ngOnInit(){
    var account:User = await firstValueFrom(this.account.getCurrentUser());

    this.email = account.email;
    this.username = account.userName;
    this.avatarUrl = account.userAvatarUrl;
    if(account.moreInfo==""){
      this.moreInfo = "Click here to write a short description about yourself"
    } else this.moreInfo = account.moreInfo;

    await (await this.toast.create({
      cssClass: 'mytoast',
      message: "Did you know you can click on your profile picture to change it?",
      icon: "information-circle-outline",
      duration: 5000
    })).present();
    await this.fetchRecipes();

  }


  async saveChanges() {
    this.isEditMode = false;
    this.moreInfo = this.editedDescription;

    var user:User = await firstValueFrom(this.account.getCurrentUser());
    user.moreInfo = this.editedDescription;
    console.log(user);
    const responst = await this.account.update(user);
    const data = await firstValueFrom(responst);

  }

  clickEdit() {
    this.isEditMode = true;
    this.editedDescription = this.moreInfo;
  }

  async onFileSelected($event: Event) {
    const target = $event.target as HTMLInputElement;
    const file: File = (target.files as FileList)[0];
    const responst= await this.account.updateAvatar(file);
    const data = await firstValueFrom(responst);
    location.reload();
  }

  async fetchRecipes() {
    var recipes = await firstValueFrom(await this.account.getUserRecipes());
    this.amountOfRecipes = recipes.length;
    console.log(recipes);
  }
}
