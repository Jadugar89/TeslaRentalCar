import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ICar, ICarDetail } from '../shared/interface';


@Injectable({
  providedIn: 'root'
})
export class CarService {

    baseUrl: string = 'https://localhost:7236/api/car/';
    
    constructor(private http: HttpClient) { }


    getCars() : Observable<ICar[]> {
        return this.http.get<ICar[]>(this.baseUrl)
            .pipe(
              catchError(err => {
                console.log('Handling error locally and rethrowing it...', err);
                return throwError(err);
            })
          )}

    getCar(id: number) : Observable<ICarDetail> {
            return this.http.get<ICarDetail>(this.baseUrl +id)
              .pipe(
                catchError(err =>{
                  console.log('Handling error locally and rethrowing it...', err);
                  return throwError(err);
                })
          )}


}