import { Component, OnInit } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {FormsModule} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-recipe-menu',
  standalone: true,
  imports: [
    IonicModule,
    FormsModule
  ],
  templateUrl: './recipe-menu.component.html',
  styleUrls: ['./recipe-menu.component.scss'],
})
export class RecipeMenuComponent  implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {}

  handleImageChange($event: Event) {

  }

  addIngredient() {

  }

  addStep() {

  }

  saveRecipe() {

  }

  clickCancel() {
    this.router.navigate(['/home'])
  }
}
