import { Component, OnInit } from '@angular/core';
import {IonicModule, ToastController} from "@ionic/angular";
import {Router} from "@angular/router";
import {TokenService} from "../../../../services/token.service";
import {User} from "../../../models";
import {firstValueFrom} from "rxjs";
import {AccountService} from "../../../../services/account.service";
import {HttpErrorResponse} from "@angular/common/http";

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
    private router : Router, public accountService : AccountService) {}

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

  async clickProfile() {
    try {
      var account: User = await firstValueFrom(this.accountService.getCurrentUser());
      this.router.navigate(['/home/profile', account.userId], {replaceUrl: true})
    } catch (e) {
      if (e instanceof HttpErrorResponse && e.status == 401) {
        await this.router.navigate(['/login'], {replaceUrl: true});
      }
    }
  }
  clickSearch() {
    this.router.navigate(['/home/search'], {replaceUrl:true})
  }

}
