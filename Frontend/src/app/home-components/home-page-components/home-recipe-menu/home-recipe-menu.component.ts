import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {UserRecipeComponent} from "../../recipe-components/user-recipe/user-recipe.component";
import {Recipe} from "../../../models";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";
import {CommonModule} from "@angular/common";
import {environment} from "../../../../environments/environment";

@Component({
  selector: 'app-home-recipe-menu',
  standalone: true,
  imports: [
    IonicModule,
    UserRecipeComponent, CommonModule
  ],
  templateUrl: './home-recipe-menu.component.html',
  styleUrls: ['./home-recipe-menu.component.css']
})
export class HomeRecipeMenuComponent {

  constructor(private http : HttpClient) {
    this.getRandomRecipes();
  }

  randomRecipes: Recipe[] = [];
  async getRandomRecipes() {
    try {
      const call = this.http.get<Recipe[]>(environment.baseUrl +'/api/random/recipes');
      this.randomRecipes = await firstValueFrom<Recipe[]>(call);
    } catch (error) {
      console.error('Error fetching random recipes:', error);
    }
  }

  clickRefresh(){
    this.getRandomRecipes();
  }
}
