import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {HomeRecipeComponent} from "../../recipe-components/home-recipe/home-recipe.component";
import {UserRecipeComponent} from "../../recipe-components/user-recipe/user-recipe.component";
import {Recipe} from "../../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../../services/recipe.service";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-home-menu',
  standalone: true,
  imports: [
    IonicModule,
    HomeRecipeComponent,
    UserRecipeComponent, CommonModule
  ],
  templateUrl: './home-menu.component.html',
  styleUrls: ['./home-menu.component.css']
})
export class HomeMenuComponent {

  constructor(private http : HttpClient, public recipeService : RecipeService) {
    this.getData();
  }

  async getData() {
    const call = this.http.get<Recipe[]>('http://localhost:5280/api/recipes');
    this.recipeService.recipes = await firstValueFrom<Recipe[]>(call);
  }
}
