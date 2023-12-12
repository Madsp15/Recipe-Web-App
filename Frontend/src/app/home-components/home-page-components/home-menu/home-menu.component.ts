import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {HomeRecipeComponent} from "../home-recipe/home-recipe.component";
import {UserRecipeComponent} from "../../user-recipe/user-recipe.component";

@Component({
  selector: 'app-home-menu',
  standalone: true,
  imports: [
    IonicModule,
    HomeRecipeComponent,
    UserRecipeComponent
  ],
  templateUrl: './home-menu.component.html',
  styleUrls: ['./home-menu.component.css']
})
export class HomeMenuComponent {

}
