import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../authentication/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private auth:AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
       
    return next.handle(this.getAuthorizedRequest(req))
  }

  getAuthorizedRequest(req:HttpRequest<any>){
    return req.clone({
      setHeaders:{
        'Authorization': 'Bearer ' + this.auth.token
      }
    })
  }
}
