import { Injectable } from '@angular/core';
import {Recipe} from "../app/models";
import {FormGroup} from "@angular/forms";


@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  readonly storedIFormFile: File[] = [];
  public recipes: Recipe[] = [];
  public currentRecipe: Recipe = {};
  public formGroup: FormGroup | null = new FormGroup<any>({
 });

  setFormGroup(formGroup: FormGroup) {
    this.formGroup = formGroup;
  }

  getFormGroup(): FormGroup | null {
    return this.formGroup;
  }
}
