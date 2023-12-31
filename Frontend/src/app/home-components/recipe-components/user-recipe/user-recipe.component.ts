import {Component, Input, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AlertController, IonicModule} from "@ionic/angular";
import {RecipeService} from "../../../../services/recipe.service";
import {Recipe, User} from "../../../models";
import {firstValueFrom} from "rxjs";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {RatingComponent} from "../../review-components/rating/rating.component";
import {UserService} from "../../../../services/user.service";
import {AccountService} from "../../../../services/account.service";
import {environment} from "../../../../environments/environment";

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
              public userService : UserService,
              public accountService : AccountService,
              private alertController : AlertController) {
  }

  ngOnInit(): void {
    this.getAverageRating(this.recipe?.recipeId);
    this.getUser(this.recipe?.userId);
    this.checkIsCurrentUserRecipe();
  }

  averageRating: number | null = null;
  isCurrentUserRecipe = false;

  async getAverageRating(recipeId: number | undefined) {
    try {
      const response = await firstValueFrom(this.http.get<any>(environment.baseUrl +'/api/recipe/averagerating/'+recipeId));
      this.averageRating = response;
    } catch (e) {
    }
  }

  getUser(userId: number | undefined) {
    const user = this.userService.getUserByIdFromList(userId);
    return user ? user.userName : 'Unknown User';
  }

  async clickRecipe(recipeId?: number) {
    this.router.navigate(['home/recipedetails/', recipeId], {replaceUrl:true})
  }

  clickUser(userId: number | undefined) {
    this.router.navigate(['home/profile/', userId], {replaceUrl:true})
  }

  editRecipe(recipe: Recipe | undefined) {
    if (recipe) {
      this.recipeService.currentRecipe = recipe;
      this.recipeService.isEdit = true;
      this.router.navigate(['home/create-recipe'], {replaceUrl:true});
    }
  }

  async deleteRecipe(recipe: Recipe | undefined) {
    const alert = await this.alertController.create({
      header: 'Delete Recipe',
      message: 'Are you sure you want to delete '+this.recipe?.title+'? Changes cannot be reverted.',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: () => {
            console.log('Cancel clicked');
          }
        },
        {
          text: 'Yes',
          handler: async () => {
            const response =  firstValueFrom(this.http.delete<any>(environment.baseUrl +'/api/recipes/'+recipe?.recipeId));
            console.log("Delete response: "+response)
            location.reload();
            }
          }
      ]
    });
    await alert.present();
  }

  async checkIsCurrentUserRecipe() {
    const account: User = await firstValueFrom(this.accountService.getCurrentUser());
    console.log("Logged in user: " + account.userId);
    console.log("Review user id: " + this.recipe?.userId);

    this.isCurrentUserRecipe = account.userId === this.recipe?.userId;
  }

  getAvatarUrl(userId: number | undefined) {
    const user = this.userService.getUserByIdFromList(userId);
    return user ? user.userAvatarUrl : 'Unknown File';
  }

  clickTitle(recipeId: number | undefined) {
    this.router.navigate(['home/recipedetails/', recipeId], {replaceUrl:true})
  }
}
