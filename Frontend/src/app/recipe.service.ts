import { Injectable } from '@angular/core';
import {Recipe} from "./models";
import {FormGroup} from "@angular/forms";


@Injectable({
  providedIn: 'root'
})
export class RecipeService {


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
