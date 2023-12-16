import {Component, Input} from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {Review} from "../../../models";
import {RatingComponent} from "../rating/rating.component";

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
}
