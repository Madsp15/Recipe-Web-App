import { Component } from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../recipe.service";
import {Recipe} from "../../../models";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-recipe-menu-steps-ingredients',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule, CommonModule, ReactiveFormsModule
  ],
  templateUrl: './recipe-menu-steps-ingredients.component.html',
  styleUrls: ['./recipe-menu-steps-ingredients.component.css']
})
export class RecipeMenuStepsIngredientsComponent {

  instructions: string[] = [];
  ingredients: string[] = [];
  newInstruction: string = '';
  newIngredient: string = '';
  editingIndex: number | null = null;

  constructor(private router: Router, private http: HttpClient, public recipeService : RecipeService, public toastController : ToastController) {
  }

  serializeData(): string {
    const serializedData = {
      instructions: JSON.stringify(this.instructions),
      ingredients: JSON.stringify(this.ingredients)
    };

    return JSON.stringify(serializedData);
  }

  deserializeData(data: string): void {
    try {
      const parsedData = JSON.parse(data);

      // Check if the parsed data has the expected properties
      if (parsedData && parsedData.instructions && parsedData.ingredients) {
        this.instructions = JSON.parse(parsedData.instructions);
        this.ingredients = JSON.parse(parsedData.ingredients);
      } else {
        console.error('Unexpected data format:', parsedData);
      }
    } catch (error) {
      console.error('Error parsing data:', error);
    }
  }

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

  async clickNext() {
    console.log(this.recipeService.getFormGroup()?.value)
    try {
      const call = this.http.post<Recipe>('http://localhost:5280/api/recipes', this.recipeService.getFormGroup()?.getRawValue());
      const result = await firstValueFrom<Recipe>(call)
      this.recipeService.recipes.push(result);
      const toast = await this.toastController.create({
        color: 'success',
        duration: 2000,
        message: "Success"
      })
      toast.present();
    } catch (error: any) {
      console.log(error);
    }
    const data = this.serializeData();
    console.log(data);
  }

  clickEditInstruction(i: number) {
    this.editingIndex = i;
    this.newInstruction = this.instructions[i];
  }

  clickEditIngredient(i: number) {
    this.editingIndex = i;
    this.newIngredient = this.ingredients[i];
  }

  clickSaveEditInstruction() {
    if (this.editingIndex !== null) {
      this.instructions[this.editingIndex] = this.newInstruction.trim();
      this.editingIndex = null;
      this.newInstruction = '';
    }
  }

  clickSaveEditIngredient() {
    if (this.editingIndex !== null) {
      this.ingredients[this.editingIndex] = this.newIngredient.trim();
      this.editingIndex = null;
      this.newIngredient = '';
    }
  }

  clickDeleteInstruction(index: number) {
    this.instructions.splice(index, 1);
  }

  clickDeleteIngredient(index: number) {
    this.ingredients.splice(index, 1);
  }
}
