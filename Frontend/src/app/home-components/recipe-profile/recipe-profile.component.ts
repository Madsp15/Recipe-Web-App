import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule, ToastController} from "@ionic/angular";
import {UserRecipeComponent} from "../user-recipe/user-recipe.component";
import {Router} from "@angular/router";
import {TokenService} from "../../../services/token.service";
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
  constructor(private router : Router, private tokenService: TokenService,
              private readonly http: HttpClient,
              private token: TokenService,
              private account: AccountService,) {}



  isEditMode = false;
  moreInfo: string = '';
  editedDescription: string | null = '';
  username: string = 'Bob';
  email: string = '';
  avatarUrl: string | null = '';


  async ngOnInit(){
    var account:User = await firstValueFrom(this.account.getCurrentUser());

    this.email = account.email;
    this.username = account.userName;
    this.avatarUrl = account.userAvatarUrl;
    if(account.moreInfo==""){
      this.moreInfo = "Write a bit about yourself here!"
    } else this.moreInfo = account.moreInfo;

  }

  saveChanges() {
    this.isEditMode = false;
    //this.moreInfo = this.editedDescription;
  }

  clickEdit() {
    this.isEditMode = true;
    this.editedDescription = this.moreInfo;
  }
}
