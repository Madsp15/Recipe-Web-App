import {Component, OnInit} from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {ReviewCardComponent} from "../review-card/review-card.component";
import {CommonModule} from "@angular/common";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {RecipeService} from "../../../../services/recipe.service";
import {Recipe, Review, User} from "../../../models";
import {RatingComponent} from "../rating/rating.component";
import {AccountService} from "../../../../services/account.service";

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
    console.log(this.doesUserReviewExistBool)
    for (let i = 0; i < 5; i++) {
      this.stars.push('/assets/icon/empty-star.png');
    }

  }
  reviewInput = new FormControl('', Validators.required);
  selectedStarsInput = new FormControl(0, Validators.required);
  userIdInput = new FormControl(0, Validators.required);
  doesUserReviewExistBool: boolean = false;

  formGroup = new FormGroup({
    userId: this.userIdInput,
    recipeId: new FormControl(),
    comment: this.reviewInput,
    rating: this.selectedStarsInput,
  });

  rating: number = 0;
  reviews: any[] = []
  currentPage: number = 1;
  reviewsPerPage: number = 5;
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
      this.recipeService.reviews = (await firstValueFrom(this.http.get<any>('http://localhost:5280/api/reviews/' + id)));

    } catch (e) {
      this.router.navigate(['']);
    }
  }

  async getAverageRating() {
    try {
      const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
      console.log(id);
      const response = await firstValueFrom(this.http.get<any>('http://localhost:5280/api/recipe/averagerating/' + id));

      this.averageRating = response.toFixed(1);
      console.log("Average rating: "+this.averageRating)
      console.log("API response: "+response)

    } catch (e) {
    }
  }

  async getReviews() {
    try {
    const id = (await firstValueFrom(this.route.paramMap)).get('recipeid');
    const response = await firstValueFrom(this.http.get<Review[]>('http://localhost:5280/api/reviews/' + id));
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
      const response = await firstValueFrom(this.http.get<any>('http://localhost:5280/api/reviews/' + recipeId + '/' + account.userId));

      this.doesUserReviewExistBool = response;
    } catch (error) {
      console.error('Error fetching review:', error);
    }
  }


  loadPreviousReviews() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  loadNextReviews() {
    const maxPage = Math.ceil(this.reviews.length / this.reviewsPerPage);
    if (this.currentPage < maxPage) {
      this.currentPage++;
    }
  }

  async clickSubmitReview() {
    if(!this.doesUserReviewExistBool) {
      try {
        const recipeIdString = (await firstValueFrom(this.route.paramMap)).get('recipeid');
        const recipeId = parseInt(<string>recipeIdString);
        var account: User = await firstValueFrom(this.accountService.getCurrentUser());
        this.formGroup.get('userId')?.setValue(account.userId ? account.userId : 0);

        this.formGroup.patchValue({recipeId});
        this.formGroup.patchValue({rating: this.selectedStars});

        console.log(JSON.stringify(this.formGroup.getRawValue(), null, 2));

        const call = this.http.post<Review>('http://localhost:5280/api/reviews', this.formGroup.getRawValue());
        const result = await firstValueFrom<Review>(call)
        this.recipeService.reviews.push(result);
        const toast = await this.toastController.create({
          color: 'success',
          duration: 2000,
          message: "Success"
        })
        toast.present();
      } catch (error: any) {
        console.log(error);
      }
    }
    else{
      const toast = await this.toastController.create({
        color: 'danger',
        duration: 2000,
        message: "You have already reviewed this recipe"
      })
      toast.present();
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
