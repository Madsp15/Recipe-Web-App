import { Component } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {HomeRecipeComponent} from "../../recipe-components/home-recipe/home-recipe.component";
import {UserRecipeComponent} from "../../recipe-components/user-recipe/user-recipe.component";

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
