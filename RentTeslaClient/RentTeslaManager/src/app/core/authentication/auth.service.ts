import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthApiService  } from './auth-api.service'
import { ILogin } from './models/login';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  private readonly TOKEN_NAME = 'token';
  isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private authApiService: AuthApiService) {
    this._isLoggedIn$.next(!!this.token);
  }

  get token(): any {
    return localStorage.getItem(this.TOKEN_NAME);
  }

  login(credensials:ILogin) {
    return this.authApiService.login(credensials).pipe(
      tap((token:string)=>{
      this._isLoggedIn$.next(true);
      localStorage.setItem(this.TOKEN_NAME, token);
    })
  )}

  logOut(){
    this._isLoggedIn$.next(false);
    localStorage.removeItem(this.TOKEN_NAME);
  }

}
