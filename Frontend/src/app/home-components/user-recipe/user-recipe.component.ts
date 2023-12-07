import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";

@Component({
  selector: 'app-user-recipe',
  standalone: true,
  imports: [CommonModule, IonicModule],
  templateUrl: './user-recipe.component.html',
  styleUrls: ['./user-recipe.component.css']
})
export class UserRecipeComponent {

}
