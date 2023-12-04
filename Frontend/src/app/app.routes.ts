import { Routes } from '@angular/router';
import {RecipeMenuComponent} from "./recipe-menu/recipe-menu.component";
import {LoginPageComponent} from "./login-page/login-page.component";

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () => import('./home/home.page').then((m) => m.HomePage),
  },
  { path: 'create-recipe', component: RecipeMenuComponent },
  { path: 'login', component: LoginPageComponent },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
];
