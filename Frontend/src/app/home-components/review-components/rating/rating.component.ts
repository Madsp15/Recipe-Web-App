import {Component, EventEmitter, Input, Output} from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {CommonModule, NgForOf, NgStyle} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-rating',
  standalone: true,
  imports: [
    IonicModule,
    NgForOf, FormsModule, NgStyle, CommonModule
  ],
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent {
  @Input() rating: number = 0;

  get wholeStars(): number[] {
    return Array(Math.floor(this.rating)).fill(0);
  }

  get hasHalfStar(): boolean {
    return this.rating % 1 >= 0.5;
  }

  get emptyStars(): number[] {
    const emptyStarsCount = 5 - Math.ceil(this.rating);
    return Array(emptyStarsCount).fill(0);
  }
}

