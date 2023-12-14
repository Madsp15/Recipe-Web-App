import { Component } from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../recipe.service";
import {Ingredients, Recipe} from "../../../models";
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
  ingredients: Ingredients[] = [];
  newInstruction: string = '';
  newIngredient = { quantity: 0, unit: '', ingredientName: '' };



  constructor(private router: Router, private http: HttpClient, public recipeService : RecipeService, public toastController : ToastController) {
  }

  serializeData(): string {
    const serializedData = {
      instructions: JSON.stringify(this.instructions),
    };

    return JSON.stringify(serializedData);
  }

  deserializeData(data: string): void {
    try {
      const parsedData = JSON.parse(data);

      if (parsedData && parsedData.instructions) {
        this.instructions = JSON.parse(parsedData.instructions);
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
    const newIngredient = { ...this.newIngredient }; // Create a copy of the new ingredient
    this.ingredients.push(newIngredient); // Add the new ingredient to the array
    this.newIngredient = { quantity: 0, unit: '', ingredientName: '' }; // Reset for the next entry
  }



  clickCancel() {

  }

  async clickNext() {
    const data = this.serializeData();
    console.log(this.recipeService.getFormGroup()?.value)
    const recipeFormGroup = this.recipeService.getFormGroup();
    recipeFormGroup?.get('instructions')?.setValue(data);
    recipeFormGroup?.get('ingredients')?.setValue(this.ingredients);
    console.log('Full Form Group:', JSON.stringify(recipeFormGroup?.getRawValue(), null, 2));


    /*try {
      const call = this.http.post<Recipe>('http://localhost:5280/api/recipes', recipeFormGroup?.getRawValue());
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
    }*/
  }

  clickEditInstruction(i: number) {

  }


  clickEditIngredient() {
  }

  clickDeleteInstruction(index: number) {
    this.instructions.splice(index, 1);
  }

  clickDeleteIngredient(index: number) {
    this.ingredients.splice(index, 1);
  }
}
