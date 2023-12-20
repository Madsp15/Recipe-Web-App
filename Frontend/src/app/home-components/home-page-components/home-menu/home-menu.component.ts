import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {HomeRecipeComponent} from "../../recipe-components/home-recipe/home-recipe.component";
import {UserRecipeComponent} from "../../recipe-components/user-recipe/user-recipe.component";
import {Recipe} from "../../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {RecipeService} from "../../../../services/recipe.service";
import {CommonModule} from "@angular/common";
import {environment} from "../../../../environments/environment";

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

  newRecipeList: Recipe[] = [];

  constructor(private http: HttpClient, public recipeService: RecipeService) {
    this.getData();
  }

  async getData() {
    const call = this.http.get<Recipe[]>(environment.baseUrl +'/api/recipes');
    const recipes = await firstValueFrom<Recipe[]>(call);

    recipes.sort((a, b) => {
      const dateA = new Date(this.parseDate(a.dateCreated ?? ''));
      const dateB = new Date(this.parseDate(b.dateCreated ?? ''));

      return dateB.getTime() - dateA.getTime();
    });

    // Take the first 6 recipes
    this.newRecipeList = recipes.slice(0, 6);

  }

  parseDate(dateString: string): string {
    const [day, month, year] = dateString.split('-');
    return `${month}-${day}-${year}`;
  }
}
