import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {Recipe, User} from "../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../services/recipe.service";
import {UserService} from "../../../services/user.service";
import {FormsModule} from "@angular/forms";
import {UserRecipeComponent} from "../recipe-components/user-recipe/user-recipe.component";

@Component({
  selector: 'app-recipe-search',
  standalone: true,
  imports: [CommonModule, IonicModule, FormsModule, UserRecipeComponent],
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.css']
})
export class RecipeSearchComponent {

  filteredRecipeList: Recipe[] = [];
  selectedFilter: string = 'option1';
  searchQuery: string = '';

  constructor(private http: HttpClient, public recipeService: RecipeService,
              public userService: UserService) {
    this.getRecipes();
    console.log(this.selectedFilter)
  }

  clickSearch() {
    if (this.selectedFilter === 'option1') {
      this.filteredRecipeList = this.recipeService.recipes.filter(recipe => {
        return recipe?.title?.toLowerCase().includes(this.searchQuery.toLowerCase());
      });
    } else if (this.selectedFilter === 'option2') {
      const matchingUsers: User[] = this.userService.users.filter(user => {
        return user.userName?.toLowerCase().includes(this.searchQuery);
      });

      // Get recipes belonging to the matching user(s)
      this.filteredRecipeList = [];
      for (const user of matchingUsers) {
        const userRecipes = this.recipeService.recipes.filter(recipe => {
          return recipe.userId === user.userId;
        });
        this.filteredRecipeList.push(...userRecipes);
      }
    }
  }

  async getRecipes(){
    const call = this.http.get<Recipe[]>('http://localhost:5280/api/recipes');
    this.recipeService.recipes = await firstValueFrom<Recipe[]>(call);
  }
}

