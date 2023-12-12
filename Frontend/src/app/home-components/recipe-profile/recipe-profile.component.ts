import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {UserRecipeComponent} from "../user-recipe/user-recipe.component";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-recipe-profile',
  standalone: true,
  imports: [CommonModule, IonicModule, UserRecipeComponent, FormsModule],
  templateUrl: './recipe-profile.component.html',
  styleUrls: ['./recipe-profile.component.css']
})
export class RecipeProfileComponent {

  isEditMode = false;
  description = '';
  editedDescription = '';


  saveChanges() {
    this.isEditMode = false;
    this.description = this.editedDescription;
  }

  clickEdit() {
    this.isEditMode = true;
    this.editedDescription = this.description;
  }
}
