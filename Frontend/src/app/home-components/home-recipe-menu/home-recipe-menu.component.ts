import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {UserRecipeComponent} from "../user-recipe/user-recipe.component";

@Component({
  selector: 'app-home-recipe-menu',
  standalone: true,
  imports: [
    IonicModule,
    UserRecipeComponent
  ],
  templateUrl: './home-recipe-menu.component.html',
  styleUrls: ['./home-recipe-menu.component.css']
})
export class HomeRecipeMenuComponent {

}
