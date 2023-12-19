import {Component, OnInit} from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../../services/recipe.service";
import {Ingredients, Recipe, RecipeIngredient} from "../../../models";
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
export class RecipeMenuStepsIngredientsComponent implements OnInit{

  instructions: string[] = [];
  ingredients: Ingredients[] = [];
  newInstruction: string = '';
  newIngredient = { quantity: 0, unit: '', ingredientName: '' };



  constructor(private router: Router, private http: HttpClient, public recipeService : RecipeService, public toastController : ToastController) {
  }

  ngOnInit() {
    if(this.recipeService.isEdit) {
      this.getIngredients();
      this.getInstructions();
    }
  }


  async getIngredients() {
    try {
      const id = this.recipeService.currentRecipe.recipeId;
      const call = `http://localhost:5280/api/recipeingredients/recipe/${id}`;
      this.ingredients = await firstValueFrom(this.http.get<Ingredients[]>(call));
      console.log('Recipe Ingredients:', this.ingredients);
    } catch (error) {
      console.error('Error fetching recipe ingredients:', error);
      throw error;
    }
  }

  async getInstructions(): Promise<void> {
    try {
      const id = this.recipeService.currentRecipe.recipeId;
      const call = `http://localhost:5280/api/recipes/${id}`;
      const response = await firstValueFrom(this.http.get<any>(call));

      const parsedResponse = JSON.parse(response.instructions);

      this.instructions = JSON.parse(parsedResponse.instructions);

      console.log('Recipe Instructions:', this.instructions);
    } catch (error) {
      console.error('Error fetching recipe instructions:', error);
      throw error;
    }
  }

  serializeData(): string {
    const serializedData = {
      instructions: JSON.stringify(this.instructions),
    };

    return JSON.stringify(serializedData);
  }

  addInstruction() {
    if (this.newInstruction.trim() !== '') {
      this.instructions = [...this.instructions, this.newInstruction.trim()];
      this.newInstruction = '';
    }
    console.log(this.instructions)
  }

  async addIngredient() {
      const newIngredient = {...this.newIngredient}; // Create a copy of the new ingredient
      this.ingredients.push(newIngredient); // Add the new ingredient to the array
      this.newIngredient = {quantity: 0, unit: '', ingredientName: ''}; // Reset for the next entry
    if (this.recipeService.isEdit) {
      try {
        const id = this.recipeService.currentRecipe.recipeId;
        const call = `http://localhost:5280/api/add/ingredient/recipe/${id}`;
        const result = await firstValueFrom(this.http.post<Ingredients[]>(call, newIngredient, { responseType: 'json' }));
        console.log('Recipe Ingredients:', this.ingredients);
      } catch (error) {
        console.error('Error fetching recipe ingredients:', error);
        throw error;
      }
    }
  }



  clickCancel() {
    const recipeFormGroup = this.recipeService.getFormGroup();
    console.log('Full Form Group Created: ', JSON.stringify(recipeFormGroup?.getRawValue(), null, 2));
  }

  async clickNext() {
    if (this.recipeService.isEdit) {
      const data = this.serializeData();
      console.log(this.recipeService.getFormGroup()?.value)
      const recipeFormGroup = this.recipeService.getFormGroup();
      recipeFormGroup?.get('instructions')?.setValue(data);
      console.log('Full Form Group Edit: ', JSON.stringify(recipeFormGroup?.getRawValue(), null, 2));

      try {
        const call = this.http.put<Recipe>('http://localhost:5280/api/recipes', recipeFormGroup?.getRawValue());
        const result = await firstValueFrom<Recipe>(call)
        this.recipeService.recipes.push(result);

        const toast = await this.toastController.create({
          color: 'success',
          duration: 2000,
          message: "Successfully updated recipe"
        })
        this.recipeService.isEdit = false;
        toast.present();
        //this.router.navigate(['/home'], {replaceUrl:true});
      } catch (error: any) {
        console.log(error);
      }
    }

    else {
      const data = this.serializeData();
      console.log(this.recipeService.getFormGroup()?.value)
      const recipeFormGroup = this.recipeService.getFormGroup();
      recipeFormGroup?.get('instructions')?.setValue(data);
      console.log('Full Form Group Created: ', JSON.stringify(recipeFormGroup?.getRawValue(), null, 2));

      try {
        const call = this.http.post<Recipe>('http://localhost:5280/api/recipes', recipeFormGroup?.getRawValue());
        const result = await firstValueFrom<Recipe>(call)
        this.recipeService.recipes.push(result);

        if (result.recipeId != null) {
          const response = await this.uploadPicture(result.recipeId);
          const data = await firstValueFrom(response);
        }
        const toast = await this.toastController.create({
          color: 'success',
          duration: 2000,
          message: "Successfully created recipe"
        })
        toast.present();
        this.router.navigate(['/home'], {replaceUrl: true});
      } catch (error: any) {
        console.log(error);
      }
    }
  }

  clickDeleteInstruction(index: number) {
    this.instructions.splice(index, 1);
  }

  clickDeleteIngredient(index: number) {
    if(this.recipeService.isEdit){
      const ingredient = this.ingredients[index].ingredientId;
      const recipeId = this.recipeService.currentRecipe.recipeId;
      const call = firstValueFrom(this.http.delete<any>('http://localhost:5280/api/recipeingredients/recipe/'+ingredient+'/'+recipeId));
    }
      this.ingredients.splice(index, 1);
    if(this.ingredients[index].ingredientId == undefined){
      this.ingredients.splice(index, 1);
    }
  }


  uploadPicture(id: number) {
    const formData = new FormData();
    formData.append('picture', this.recipeService.storedIFormFile[0]);
    return this.http.put<Recipe>('http://localhost:5280/api/recipes/picture/'+id, formData,{
    });
  }
}
