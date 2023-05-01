import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILogin } from './models/login';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root',
})
export class AuthApiService {
  baseUrl: string =environment.baseUrl+'/api/auth/';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };


  constructor(private http: HttpClient) {}

  login(login:ILogin) {
    return this.http.post<string>(this.baseUrl+'login', login,this.httpOptions);
  }

}
