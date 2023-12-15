import { Routes } from '@angular/router';
import { RecipeMenuComponent } from "./home-components/create-edit-recipe/recipe-menu/recipe-menu.component";
import { LoginPageComponent } from "./login-components/login-page/login-page.component";
import { LoginComponent } from "./login-components/login/login.component";
import { SignUpComponent } from "./login-components/sign-up/sign-up.component";
import { ForgotPasswordComponent } from "./login-components/forgot-password/forgot-password.component";
import {HomePage} from "./home-components/home-page-components/home/home.page";
import {RecipeProfileComponent} from "./home-components/recipe-profile/recipe-profile.component";
import {RecipeSearchComponent} from "./home-components/recipe-search/recipe-search.component";
import {HomeMenuComponent} from "./home-components/home-page-components/home-menu/home-menu.component";
import {
  RecipeMenuStepsIngredientsComponent
} from "./home-components/create-edit-recipe/recipe-menu-steps-ingredients/recipe-menu-steps-ingredients.component";
import {HomeRecipeMenuComponent} from "./home-components/home-page-components/home-recipe-menu/home-recipe-menu.component";
import {RecipeComponent} from "./home-components/recipe-components/recipe/recipe.component";
import {AuthenticatedGuard} from "./Guards";

export const routes: Routes = [
  { path: 'create-recipe', component: RecipeMenuComponent },
  {
    path: 'login',
    component: LoginPageComponent,
    children: [
      { path: '', component: LoginComponent },
      { path: 'sign-up', component: SignUpComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
    ]
  },
  {
    path: 'home',
    component: HomePage,
    children: [
      { path: '', component: HomeMenuComponent },
      { path: 'create-recipe', component: RecipeMenuComponent,canActivate:[AuthenticatedGuard] },
      { path: 'instructions-ingredients', component: RecipeMenuStepsIngredientsComponent },
      { path: 'recipes', component: HomeRecipeMenuComponent },
      { path: 'profile', component: RecipeProfileComponent,canActivate:[AuthenticatedGuard] },
      { path: 'search', component: RecipeSearchComponent },
      { path: 'recipedetails/:recipeid', component: RecipeComponent },
    ]
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
];

