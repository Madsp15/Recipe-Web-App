import {Component, Input} from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {Review, User} from "../../../models";
import {RatingComponent} from "../rating/rating.component";
import {AccountService} from "../../../../services/account.service";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-review-card',
  standalone: true,
  imports: [
    IonicModule,
    RatingComponent
  ],
  templateUrl: './review-card.component.html',
  styleUrls: ['./review-card.component.css']
})
export class ReviewCardComponent {

  @Input() review: Review | undefined;

  constructor(public accountService : AccountService) {
  }

  clickDeleteReview() {

  }

  async isCurrentUserReview(userId: number | undefined) {
    var account:User = await firstValueFrom(this.accountService.getCurrentUser());
    console.log("Logged in user: "+account.userId)
    console.log("Review userid: "+userId)
    return account.userId === userId;
  }
}
