import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {RecipeService} from "../../../recipe.service";
import {Recipe} from "../../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-user-recipe',
  standalone: true,
  imports: [CommonModule, IonicModule, HttpClientModule, FormsModule],
  templateUrl: './user-recipe.component.html',
  styleUrls: ['./user-recipe.component.css']
})
export class UserRecipeComponent {

  constructor(public recipeService: RecipeService, private http : HttpClient, private router : Router, public route: ActivatedRoute) {
    this.getData();
  }

  async getData() {
    const call = this.http.get<Recipe[]>('http://localhost:5280/api/recipes');
    this.recipeService.recipes = await firstValueFrom<Recipe[]>(call);
  }

  async clickRecipe(recipeId?: number) {
    console.log(recipeId)
    this.router.navigate(['home/recipedetails/', recipeId], {replaceUrl:true})
  }

  onCardClick(recipe: Recipe) {
    console.log('Clicked Recipe:', recipe);
  }
}
