import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {Recipe, User} from "../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../services/recipe.service";
import {FormsModule} from "@angular/forms";
import {UserRecipeComponent} from "../recipe-components/user-recipe/user-recipe.component";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-recipe-search',
  standalone: true,
  imports: [CommonModule, IonicModule, FormsModule, UserRecipeComponent],
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.css']
})
export class RecipeSearchComponent {

  filteredRecipeList: Recipe[] = [];
  searchQuery: string = '';

  constructor(private http: HttpClient, public recipeService: RecipeService) {
    this.getRecipes();
  }

  clickSearch() {
      this.filteredRecipeList = this.recipeService.recipes.filter(recipe => {
        return recipe?.title?.toLowerCase().includes(this.searchQuery.toLowerCase());
      });
  }

  async getRecipes(){
    const call = this.http.get<Recipe[]>(environment.baseUrl +'/api/recipes');
    this.recipeService.recipes = await firstValueFrom<Recipe[]>(call);
  }
}

