import { Routes } from '@angular/router';
import { RecipeMenuComponent } from "./home-components/recipe-menu/recipe-menu.component";
import { LoginPageComponent } from "./login-components/login-page/login-page.component";
import { LoginComponent } from "./login-components/login/login.component";
import { SignUpComponent } from "./login-components/sign-up/sign-up.component";
import { ForgotPasswordComponent } from "./login-components/forgot-password/forgot-password.component";

export const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home-components/home/home.page').then((m) => m.HomePage),
  },
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
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
];

