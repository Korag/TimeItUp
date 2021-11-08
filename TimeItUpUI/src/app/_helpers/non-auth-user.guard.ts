import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class NonAuthUserGuard implements CanActivate {

  constructor(
    private router: Router,
    private _authService: AuthService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const loggedUser = this._authService.loggedUserData;

    if (!loggedUser) {
      return true;
    }

    this.router.navigate(['/timers/active'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
