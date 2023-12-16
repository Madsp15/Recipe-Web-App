import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {RecipeService} from "../../../../services/recipe.service";
import {Recipe} from "../../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {RatingComponent} from "../../review-components/rating/rating.component";

@Component({
  selector: 'app-user-recipe',
  standalone: true,
  imports: [CommonModule, IonicModule, HttpClientModule, FormsModule, RatingComponent],
  templateUrl: './user-recipe.component.html',
  styleUrls: ['./user-recipe.component.css']
})
export class UserRecipeComponent {

  constructor(public recipeService: RecipeService, private http : HttpClient, private router : Router, public route: ActivatedRoute) {
    this.getData();
    this.getAverageRating();
  }

  averageRating: number | null = null;


  async getData() {
    const call = this.http.get<Recipe[]>('http://localhost:5280/api/recipes');
    this.recipeService.recipes = await firstValueFrom<Recipe[]>(call);
  }

  async getAverageRating() {
    try {
      const id = this.recipeService.currentRecipe.recipeId;
      console.log(id);
      const response = await firstValueFrom(this.http.get<any>('http://localhost:5280/api/recipe/averagerating/' + id));

      this.averageRating = response;
      console.log("Average rating: "+this.averageRating)
      console.log("API response: "+response)

    } catch (e) {
    }
  }

  async clickRecipe(recipeId?: number) {
    console.log(recipeId)
    this.router.navigate(['home/recipedetails/', recipeId], {replaceUrl:true})
  }

  onCardClick(recipe: Recipe) {
    console.log('Clicked Recipe:', recipe);
  }
}
