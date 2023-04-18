import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ICar, ICarDetail } from '../shared/interface';
import { errorHandlingConfig } from 'angular';


@Injectable({
  providedIn: 'root'
})
export class CarService {

    baseUrl: string = 'https://localhost:7236/api/car/';
    
    constructor(private http: HttpClient) { }

    httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

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
            catchError(err => {
            console.log('caught mapping error and rethrowing', err);
            return throwError(()=>err);
            })
              )}

          updateCarDetail(carDetail: ICarDetail): Observable<ICarDetail> {
            return this.http.put<ICarDetail>(this.baseUrl +carDetail.id, carDetail,this.httpOptions)
            .pipe(
              catchError(err => {
              console.log('caught mapping error and rethrowing', err);
              return throwError(()=>err);
              })
          )}   
          createCarDetail(carDetail: ICarDetail): Observable<ICarDetail>{
            return this.http.post<ICarDetail>(this.baseUrl, carDetail,this.httpOptions)
            .pipe(
              catchError(err => {
              console.log('caught mapping error and rethrowing', err);
              return throwError(()=>err);
              })
          )}
          
}