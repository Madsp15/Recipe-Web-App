import {Component, Input, OnInit} from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {Review, User} from "../../../models";
import {RatingComponent} from "../rating/rating.component";
import {AccountService} from "../../../../services/account.service";
import {firstValueFrom} from "rxjs";
import {CommonModule} from "@angular/common";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-review-card',
  standalone: true,
  imports: [
    IonicModule,
    RatingComponent, CommonModule
  ],
  templateUrl: './review-card.component.html',
  styleUrls: ['./review-card.component.css']
})
export class ReviewCardComponent implements OnInit{

  @Input() review: Review | undefined;
  isCurrentUserReview: boolean = false;

  constructor(public accountService : AccountService, private http : HttpClient,
  public toastController : ToastController) {
  }

  ngOnInit() {
    this.checkIfCurrentUserReview();
  }

  async checkIfCurrentUserReview() {
    const account: User = await firstValueFrom(this.accountService.getCurrentUser());
    console.log("Logged in user: " + account.userId);
    console.log("Review user id: " + this.review?.userId);

    this.isCurrentUserReview = account.userId === this.review?.userId;
  }
  async clickDeleteReview(reviewID: number | undefined) {
    try {
      const call = this.http.delete('http://localhost:5280/api/reviews/' + reviewID);
      const response = await firstValueFrom(call);
      location.reload();
      const toast = await this.toastController.create({
        message: 'Review deleted successfully',
        duration: 2000,
        position: 'bottom'
      });
      toast.present();
      console.log('Review deleted successfully', response);
    } catch (error) {
      const toast = await this.toastController.create({
        message: 'Error deleting review',
        duration: 2000,
        position: 'bottom'
      });
      toast.present();
      console.log('Error deleting review', error);
    }
  }
}
