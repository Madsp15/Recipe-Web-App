import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {ToastController} from "@ionic/angular";
import {TokenService} from "../services/token.service";


@Injectable({
  providedIn: 'root'
})
export class AuthenticatedGuard implements CanActivate {
  constructor(
    private readonly router: Router,
    private readonly token: TokenService,
    private readonly toast: ToastController,
  ) {
  }

  //can be added to any route to make sure the user is logged in
  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean | UrlTree> {
    const isAuthenticated = !!this.token.getToken();
    if (isAuthenticated) return true;
    (await this.toast.create({
      message: 'Login required!',
       duration: 5000
    })).present();

    this.router.navigate(['/login'], {replaceUrl:true});
    return this.router.parseUrl('/login') ;
  }
}
