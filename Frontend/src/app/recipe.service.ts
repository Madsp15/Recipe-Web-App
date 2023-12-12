import { Injectable } from '@angular/core';
import {Recipe} from "./models";
import {FormControl, FormGroup} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  public title = new FormControl('')
  public userId = new FormControl(21);
  public description = new FormControl('')
  public duration = new FormControl('')
  public recipeUrl = new FormControl('')
  public selectedTags = new FormControl<string[]>([])

  public recipes: Recipe[] = [];
  public formGroup: FormGroup | null = new FormGroup<any>({
 title: this.title
  });

  setFormGroup(formGroup: FormGroup) {
    this.formGroup = formGroup;
  }

  getFormGroup(): FormGroup | null {
    return this.formGroup;
  }
}
