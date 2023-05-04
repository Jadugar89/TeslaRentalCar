import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
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
        'Content-Type': 'application/json',
      }),
    };

    getCars() : Observable<ICar[]> {
      return this.http.get<ICar[]>(this.baseUrl)
        .pipe(
          catchError(err =>this.handleError(err) )
    )}

    getCar(id: number) : Observable<ICarDetail> {
      return this.http.get<ICarDetail>(this.baseUrl+"getById/" +id)
        .pipe(
          catchError(err =>this.handleError(err) )
    )}

    updateCarDetail(carDetail: ICarDetail): Observable<ICarDetail> {
      
      return this.http.put<ICarDetail>(this.baseUrl +carDetail.id, carDetail,this.httpOptions)
        .pipe(
          catchError(err =>this.handleError(err) )
    )}  

    createCarDetail(carDetail: ICreatedCar): Observable<ICreatedCar>{
      return this.http.post<ICreatedCar>(this.baseUrl, carDetail,this.httpOptions)
        .pipe(
          catchError(err =>this.handleError(err) )
    )}

    deleteCar(id: number) {
      return this.http.delete(this.baseUrl+id).pipe(
        catchError(err =>this.handleError(err) )
    )}    
    
    private handleError(error: HttpErrorResponse) {
      if (error.status === 0) {
        // A client-side or network error occurred. Handle it accordingly.
        console.error('An error occurred:', error.error);
        
      } else {
        // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong.
        console.error(
          `Backend returned code ${error.status}, body was: `, error.error);
      }
      // Return an observable with a user-facing error message.
      return throwError(() => new Error('Something bad happened; please try again later.'));
    }
}