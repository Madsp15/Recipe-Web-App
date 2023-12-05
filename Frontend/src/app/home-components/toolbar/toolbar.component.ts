import { Component, OnInit } from '@angular/core';
import {IonicModule, MenuController} from "@ionic/angular";

@Component({
  selector: 'app-toolbar',
  standalone: true,
  imports: [
    IonicModule
  ],
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent  implements OnInit {

  constructor() {}

  clickMenu() {

    console.log("clickMenu() method clicked")
  }

  ngOnInit() {}

  clickHome() {

  }

  clickRecipes() {

  }

  clickProfile() {

  }

  clickSearch() {

  }
}
