import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICarType } from '../models/interface';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarTypeService {

  baseUrl: string = 'https://localhost:7236/api/cartype/';
    
  constructor(private http: HttpClient) { }

    getCarTypes() : Observable<ICarType[]> {
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
