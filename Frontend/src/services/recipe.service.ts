import { Injectable } from '@angular/core';
import {Recipe, Review} from "../app/models";
import {FormGroup} from "@angular/forms";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";


@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  public isEdit: boolean = false;
  readonly storedIFormFile: File[] = [];
  public recipes: Recipe[] = [];
  public reviews: Review[] = [];
  public currentRecipe: Recipe = {};
  public formGroup: FormGroup | null = new FormGroup<any>({
 });

  constructor(private http : HttpClient) {
      this.getRecipes();
  }

  setFormGroup(formGroup: FormGroup) {
    this.formGroup = formGroup;
  }

  getFormGroup(): FormGroup | null {
    return this.formGroup;
  }

    async getRecipes(){
        const call = this.http.get<Recipe[]>('http://localhost:5280/api/recipes');
        this.recipes = await firstValueFrom<Recipe[]>(call);
    }

    getRecipeByIdFromList(recipeId: number | undefined): Recipe | undefined {
        return this.recipes.find(recipe => recipe.recipeId === recipeId);
    }
}
