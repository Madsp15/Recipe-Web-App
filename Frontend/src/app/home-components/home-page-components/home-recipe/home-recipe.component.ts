import { Component, OnInit } from '@angular/core';
import {CommonModule} from "@angular/common";
import {IonicModule} from "@ionic/angular";

@Component({
  selector: 'app-home-recipe',
  standalone: true,
  imports: [CommonModule, IonicModule],
  templateUrl: './home-recipe.component.html',
  styleUrls: ['./home-recipe.component.scss'],
})
export class HomeRecipeComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
