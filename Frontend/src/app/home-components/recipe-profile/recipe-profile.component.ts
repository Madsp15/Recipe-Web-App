import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule, ToastController} from "@ionic/angular";
import {UserRecipeComponent} from "../user-recipe/user-recipe.component";
import {Router} from "@angular/router";
import {TokenService} from "../../../services/token.service";

@Component({
  selector: 'app-recipe-profile',
  standalone: true,
  imports: [CommonModule, IonicModule, UserRecipeComponent],
  templateUrl: './recipe-profile.component.html',
  styleUrls: ['./recipe-profile.component.css']
})
export class RecipeProfileComponent {
  constructor(private router : Router,
              private tokenService: TokenService) {}



}
