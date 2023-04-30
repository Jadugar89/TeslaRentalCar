import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ICar, ICarDetail } from '../shared/models/interface';
import { ICreatedCar } from './models/create-car.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CarService {

    baseUrl: string =environment.baseUrl+'/api/car/';
    
    constructor(private http: HttpClient) { }

    httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    getCars() : Observable<ICar[]> {
      return this.http.get<ICar[]>(this.baseUrl)
        .pipe(
          catchError(err => {
            console.log('Handling error locally and rethrowing it...', err);
            return throwError(()=>err);
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

    createCarDetail(carDetail: ICreatedCar): Observable<ICreatedCar>{
      return this.http.post<ICreatedCar>(this.baseUrl, carDetail,this.httpOptions)
        .pipe(
          catchError(err => {
            console.log('caught mapping error and rethrowing', err);
            return throwError(()=>err);
      })
    )}

    deleteCar(id: number) {
      return this.http.delete(this.baseUrl+id).pipe(
        catchError(err => {
          console.log('caught mapping error and rethrowing', err);
          return throwError(()=>err);
          })
    )}        
}