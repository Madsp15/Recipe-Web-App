import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-recipe-menu-steps-ingredients',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule, CommonModule
  ],
  templateUrl: './recipe-menu-steps-ingredients.component.html',
  styleUrls: ['./recipe-menu-steps-ingredients.component.css']
})
export class RecipeMenuStepsIngredientsComponent {

  instructions: string[] = [];
  ingredients: string[] = [];
  newInstruction: string = '';
  newIngredient: string = '';

  addInstruction() {
    if (this.newInstruction.trim() !== '') {
      this.instructions = [...this.instructions, this.newInstruction.trim()];
      this.newInstruction = '';
    }
    console.log(this.instructions)
  }

  addIngredient() {
    if (this.newIngredient.trim() !== '') {
      this.ingredients = [...this.ingredients, this.newIngredient.trim()];
      this.newIngredient = '';
    }
    console.log(this.ingredients)
  }

  clickCancel() {

  }

  clickNext() {

  }

  clickEditInstruction(i: number) {

  }

  clickEditIngredient(i: number) {

  }

  clickDeleteInstruction(index: number) {
    this.instructions.splice(index, 1);
  }

  clickDeleteIngredient(index: number) {
    this.ingredients.splice(index, 1);
  }
}
