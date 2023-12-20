import {Component} from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {ReviewCardComponent} from "../review-card/review-card.component";
import {CommonModule} from "@angular/common";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {RecipeService} from "../../../../services/recipe.service";
import {Review, User} from "../../../models";
import {RatingComponent} from "../rating/rating.component";
import {AccountService} from "../../../../services/account.service";
import {environment} from "../../../../environments/environment";

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [
    IonicModule,
    ReviewCardComponent, CommonModule, FormsModule, ReactiveFormsModule, RatingComponent
  ],
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent{

  constructor(private http: HttpClient,
              private route: ActivatedRoute, private router: Router,
              public recipeService: RecipeService, public toastController : ToastController,
              public accountService : AccountService) {
    this.getRecipe();
    this.getAverageRating();
    this.getReviews();
    this.doesUserReviewExist();
    this.isUserReviewValid();
    console.log("Does user exist: "+this.doesUserReviewExistBool)
    console.log("Is user valid bool: "+this.isUserReviewNotValidBool)
    for (let i = 0; i < 5; i++) {
      this.stars.push('/assets/icon/empty-star.png');
    }

  }
  reviewInput = new FormControl('', Validators.required);
  selectedStarsInput = new FormControl(0, Validators.required);
  userIdInput = new FormControl(0, Validators.required);
  doesUserReviewExistBool: boolean = false;
  isUserReviewNotValidBool: boolean = false;

  formGroup = new FormGroup({
    userId: this.userIdInput,
    recipeId: new FormControl(),
    comment: this.reviewInput,
    rating: this.selectedStarsInput,
  });

  rating: number = 0;
  stars: string[] = [];
  selectedStars: number = 0;
  averageRating: number | null = null;


  rate(index: number): void {
    // Check if the clicked star is already filled
    const isFilled = this.stars[index] === '/assets/icon/full-star.png';
    if (isFilled) {
      // Remove the last filled star when clicking on an empty star
      this.stars[this.selectedStars - 1] = '/assets/icon/empty-star.png';
      // Update selected stars count
      this.selectedStars--;
    } else {
      // Reset all stars to empty-star.png
      this.stars = this.stars.map(() => '/assets/icon/empty-star.png');

      // Set selected stars to full-star.png
      for (let i = 0; i <= index; i++) {
        this.stars[i] = '/assets/icon/full-star.png';
      }
      // Update selected stars count
      this.selectedStars = index + 1;
    }
    console.log(this.selectedStars);
  }

  async getRecipe() {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      console.log(id);
      this.recipeService.reviews = (await firstValueFrom(this.http.get<any>(environment.baseUrl +'/api/reviews/' + id)));

    } catch (e) {
      this.router.navigate(['']);
    }
  }

  async getAverageRating() {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      console.log(id);
      const response = await firstValueFrom(this.http.get<any>(environment.baseUrl +'/api/recipe/averagerating/' + id));

      this.averageRating = response.toFixed(1);
      console.log("Average rating: "+this.averageRating)
      console.log("API response: "+response)

    } catch (e) {
    }
  }

  async getReviews() {
    try {
    const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
    const response = await firstValueFrom(this.http.get<Review[]>(environment.baseUrl +'/api/reviews/' + id));
      console.log('Reviews response:', response);
      this.recipeService.reviews = response;
      console.log(this.recipeService.reviews)
      } catch (e) {
      }
  }

  async doesUserReviewExist(): Promise<void> {
    try {
      const recipeIdString = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      const recipeId = Number(recipeIdString);
      var account: User = await firstValueFrom(this.accountService.getCurrentUser());
      this.doesUserReviewExistBool = await firstValueFrom(this.http.get<any>(environment.baseUrl +'/api/reviews/' + recipeId + '/' + account.userId));
    } catch (error) {
      console.error('Error fetching review:', error);
    }
  }

  async isUserReviewValid() {
    try {
      const recipeIdString = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      const recipeId = parseInt(<string>recipeIdString);

      var account: User = await firstValueFrom(this.accountService.getCurrentUser());
      const recipe = await this.recipeService.getRecipeByIdFromList(recipeId);
      console.log("recipe:" + recipe);
      console.log("Recipe userID: "+recipe?.userId);
      console.log("Current logged in user ID: "+account.userId);

      if (recipe?.userId == account.userId) {
        this.isUserReviewNotValidBool = true;
      }
    } catch (error: any) {
      console.error('Error in isUserReviewValid:', error);
    }
  }


  async clickSubmitReview() {
    console.log("does user review exist" + this.doesUserReviewExistBool)
    console.log("is user review vaild?" + this.isUserReviewNotValidBool)
    console.log("does this user review exist?" + this.doesUserReviewExistBool)
    if(this.doesUserReviewExistBool || this.isUserReviewNotValidBool) {
      if (this.doesUserReviewExistBool) {
        const toast = await this.toastController.create({
          duration: 2000,
          message: "You have already reviewed this recipe!"
        })
        toast.present();
      }
      if (this.isUserReviewNotValidBool) {
        const toast = await this.toastController.create({
          duration: 2000,
          message: "You can't review your own recipe!"
        })
        toast.present();
      }
    }
    else{
      try {
        const recipeIdString = (await firstValueFrom(this.route.paramMap)).get('recipeid');
        const recipeId = parseInt(<string>recipeIdString);
        var account: User = await firstValueFrom(this.accountService.getCurrentUser());
        this.formGroup.get('userId')?.setValue(account.userId ? account.userId : 0);

        this.formGroup.patchValue({recipeId});
        this.formGroup.patchValue({rating: this.selectedStars});

        console.log(JSON.stringify(this.formGroup.getRawValue(), null, 2));

        const call = this.http.post<Review>(environment.baseUrl +'/api/reviews', this.formGroup.getRawValue());
        const result = await firstValueFrom<Review>(call)
        this.recipeService.reviews.push(result);
        const toast = await this.toastController.create({
          duration: 2000,
          message: "Success"
        })
        toast.present();
        //location.reload();
      } catch (error: any) {
        console.log(error);
      }
    }
  }


  async clickBack() {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      this.router.navigate(['/home/recipedetails/', id], {replaceUrl: true})
    } catch (e) {
    }
  }
}
