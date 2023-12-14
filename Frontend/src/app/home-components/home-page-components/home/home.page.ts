import { Component } from '@angular/core';
import {IonHeader, IonToolbar, IonTitle, IonContent, IonMenu, IonRouterOutlet} from '@ionic/angular/standalone';
import {ToolbarComponent} from "../../toolbar/toolbar.component";
import {SideMenuComponent} from "../../side-menu/side-menu.component";
@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  standalone: true,
  imports: [IonHeader, IonToolbar, IonTitle, IonContent, ToolbarComponent, IonMenu, SideMenuComponent, IonRouterOutlet],
})
export class HomePage {
  constructor() {}
}
