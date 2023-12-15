import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {FormsModule} from "@angular/forms";
import {firstValueFrom} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {RecipeService} from "../../../../services/recipe.service";
import {CommonModule} from "@angular/common";
import {Ingredients} from "../../../models";

@Component({
  selector: 'app-recipe',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule, HttpClientModule, CommonModule
  ],
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent {

  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient, public recipeService: RecipeService) {
    this.getRecipe();
    this.getIngredients();
    this.getInstructions();
  }

  ingredients: Ingredients[] = [];
  instructions: string[] = [];

  async getRecipe() {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      console.log(id);
      this.recipeService.currentRecipe = (await firstValueFrom(this.http.get<any>('http://localhost:5280/api/recipes/' + id)));

    } catch (e) {
      this.router.navigate(['']);
    }
  }

  async getIngredients() {
    try {
    const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
    const call = `http://localhost:5280/api/recipeingredients/recipe/${id}`;
      this.ingredients = await firstValueFrom(this.http.get<any>(call));
      console.log('Recipe Ingredients:', this.ingredients);
    } catch (error) {
      console.error('Error fetching recipe ingredients:', error);
      throw error;
    }
  }

  async getInstructions(): Promise<void> {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
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

  leaveReview() {

  }

  goBack() {

  }
}
