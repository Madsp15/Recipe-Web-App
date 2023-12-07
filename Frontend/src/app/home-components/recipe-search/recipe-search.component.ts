import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {IonicModule} from "@ionic/angular";

@Component({
  selector: 'app-recipe-search',
  standalone: true,
  imports: [CommonModule, IonicModule],
  templateUrl: './recipe-search.component.html',
  styleUrls: ['./recipe-search.component.css']
})
export class RecipeSearchComponent {

  clickSearch() {

  }
}
