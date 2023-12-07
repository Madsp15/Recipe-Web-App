import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {UserRecipeComponent} from "../user-recipe/user-recipe.component";

@Component({
  selector: 'app-recipe-profile',
  standalone: true,
  imports: [CommonModule, IonicModule, UserRecipeComponent],
  templateUrl: './recipe-profile.component.html',
  styleUrls: ['./recipe-profile.component.css']
})
export class RecipeProfileComponent {

}
