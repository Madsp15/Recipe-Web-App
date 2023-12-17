import {Component, Input, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";
import {RecipeService} from "../../../../services/recipe.service";
import {Recipe} from "../../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {RatingComponent} from "../../review-components/rating/rating.component";
import {UserService} from "../../../../services/user.service";

@Component({
  selector: 'app-user-recipe',
  standalone: true,
  imports: [CommonModule, IonicModule, HttpClientModule, FormsModule, RatingComponent],
  templateUrl: './user-recipe.component.html',
  styleUrls: ['./user-recipe.component.css']
})
export class UserRecipeComponent implements OnInit{

  @Input() recipe: Recipe | undefined;

  constructor(public recipeService: RecipeService,
              private http : HttpClient,
              private router : Router,
              public route: ActivatedRoute,
              public userService : UserService) {
  }

  ngOnInit(): void {
    this.getAverageRating(this.recipe?.recipeId);
    this.getUser(this.recipe?.userId);
  }

  averageRating: number | null = null;

  async getAverageRating(recipeId: number | undefined) {
    try {
      const response = await firstValueFrom(this.http.get<any>('http://localhost:5280/api/recipe/averagerating/'+recipeId));
      this.averageRating = response;
      console.log("Average rating: "+this.averageRating)
      console.log("API response: "+response)

    } catch (e) {
    }
  }

  getUser(userId: number | undefined) {
    const user = this.userService.getUserById(userId);
    console.log("User: "+user)
    return user ? user.userName : 'Unknown User';
  }

  async clickRecipe(recipeId?: number) {
    console.log(recipeId)
    this.router.navigate(['home/recipedetails/', recipeId], {replaceUrl:true})
  }

  clickUser(userId: number | undefined) {
    this.router.navigate(['home/profile/', userId], {replaceUrl:true})
  }
}
