import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AuthService } from '../authentication/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticatedGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
     return this.auth.isLoggedIn$.pipe(
      tap((isLoggedIn) => {
        if (!isLoggedIn) {
          this.router.navigate(['login']);
        }
      })
    );;
  }
  
  constructor(private auth:AuthService,private router:Router)
  {}
}
