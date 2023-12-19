import { Component, OnInit } from '@angular/core';
import {CommonModule} from "@angular/common";
import {IonicModule} from "@ionic/angular";
import {RatingComponent} from "../../review-components/rating/rating.component";
import {firstValueFrom} from "rxjs";
import {RecipeService} from "../../../../services/recipe.service";
import {HttpClient} from "@angular/common/http";
import {UserService} from "../../../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home-recipe',
  standalone: true,
  imports: [CommonModule, IonicModule, RatingComponent],
  templateUrl: './home-recipe.component.html',
  styleUrls: ['./home-recipe.component.scss'],
})
export class HomeRecipeComponent  implements OnInit {

  averageRating: number | null = null;

  constructor(public recipeService : RecipeService, private http : HttpClient,
  public userService : UserService, private router : Router) { }

  ngOnInit() {
    this.getRecipe();
    this.getAverageRating(this.recipeService.currentRecipe.recipeId);
  }

  async getRecipe(){
    this.recipeService.currentRecipe = (await firstValueFrom(this.http.get<any>('http://localhost:5280/api/trending/recipe')));
  }

  getUser(userId: number | undefined) {
    const user = this.userService.getUserByIdFromList(userId);
    console.log("User: "+user)
    return user ? user.userName : 'Unknown User';
  }

  async getAverageRating(recipeId: number | undefined) {
    try {
      const response = await firstValueFrom(this.http.get<any>('http://localhost:5280/api/recipe/averagerating/'+recipeId));
      this.averageRating = response;
      console.log("Average rating: "+this.averageRating)
      console.log("API response: "+response)

    } catch (e) {
    }
  }

  clickRecipeTitle() {
    this.router.navigate(['home/recipedetails/', this.recipeService.currentRecipe.recipeId], {replaceUrl:true})
  }

  clickRecipeImg() {
    this.router.navigate(['home/recipedetails/', this.recipeService.currentRecipe.recipeId], {replaceUrl:true})
  }

  clickUser(userId: number | undefined) {
    this.router.navigate(['home/profile/', userId], {replaceUrl:true})
  }
}
