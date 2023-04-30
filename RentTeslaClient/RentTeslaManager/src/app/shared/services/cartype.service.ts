import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

import { ICarType } from '../models/interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarTypeService {

  baseUrl: string = environment.baseUrl +'/api/cartype/';
    
  constructor(private http: HttpClient) { }

    getCarTypes() : Observable<ICarType[]> {
      console.log(this.baseUrl);
    return this.http.get<ICarType[]>(this.baseUrl)
        .pipe(
          catchError(err => {
            console.log('Handling error locally and rethrowing it...', err);
            return throwError(err);
        })
      )}

    getCarTypeById(id: number) : Observable<ICarType> {
        return this.http.get<ICarType>(this.baseUrl +id)
          .pipe(
            catchError(err =>{
              console.log('Handling error locally and rethrowing it...', err);
              return throwError(err);
            })
      )}
      getCarTypeByName(name: string) : Observable<ICarType> {
        return this.http.get<ICarType>(this.baseUrl +'name/'+name)
          .pipe(
            catchError(err =>{
              console.log('Handling error locally and rethrowing it...', err);
              return throwError(err);
            })
      )}

}
