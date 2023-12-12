import { Component, OnInit } from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {TokenService} from "../../../services/token.service";

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

  constructor(
    private router : Router) {}

  isRouteActive(route: string): boolean {
    return this.router.isActive(route, true);
  }

  clickMenu() {
  }

  ngOnInit() {}

  clickHome() {
    this.router.navigate([''], {replaceUrl:true})
  }

  clickRecipes() {
    this.router.navigate(['/home/recipes'], {replaceUrl:true})
  }

  clickProfile() {
    this.router.navigate(['/home/profile'], {replaceUrl:true})
  }

  clickSearch() {
    this.router.navigate(['/home/search'], {replaceUrl:true})
  }

}
