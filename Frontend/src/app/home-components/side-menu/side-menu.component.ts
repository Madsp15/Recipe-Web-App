import { Component, OnInit } from '@angular/core';
import {IonicModule} from "@ionic/angular";
import {Router} from "@angular/router";

@Component({
  selector: 'app-side-menu',
  standalone: true,
  imports: [
    IonicModule
  ],
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss'],
})
export class SideMenuComponent  implements OnInit {


  constructor(private router : Router) {}

  ngOnInit() {
  }

  clickCreateRecipe() {
    this.router.navigate(['/home/create-recipe'], {replaceUrl:true});
  }

  clickMyRecipes() {

  }

  clickSavedRecipes() {

  }

  clickLogOut() {

  }
}
