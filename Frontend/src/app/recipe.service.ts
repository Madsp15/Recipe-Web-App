import { Injectable } from '@angular/core';
import {Recipe} from "./models";

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  public recipes: Recipe[] = [];
}
