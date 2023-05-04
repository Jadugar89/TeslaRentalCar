import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICarRental } from '../shared/models/interface';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICreatedCarRental } from './models/created-car-rental.model';

@Injectable({
  providedIn: 'root'
})
export class CarRentalService {

  baseUrl: string = environment.baseUrl+'/api/CarRental/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  
  constructor(private http: HttpClient) { }

  getCarRentals() : Observable<ICarRental[]> {
    return this.http.get<ICarRental[]>(this.baseUrl+"GetAllCarRentals")
        .pipe(
          catchError(err => {
            console.log('Handling error locally and rethrowing it...', err);
           throw new Error(err);
        })
      )}

  getCarRentalById(id:Number) : Observable<ICarRental> {
    return this.http.get<ICarRental>(this.baseUrl+"GetCarRentalById/"+id)
      .pipe(
        catchError(err =>this.handleError(err))
  )}
  
  updateCarRental(carDetail: ICarRental): Observable<ICarRental> {
      
    return this.http.put<ICarRental>(this.baseUrl +carDetail.id, carDetail,this.httpOptions)
      .pipe(
        catchError(err =>this.handleError(err))
  )}  

  createCarRental(carRentalDetail: ICreatedCarRental): Observable<ICreatedCarRental>{
    return this.http.post<ICreatedCarRental>(this.baseUrl, carRentalDetail,this.httpOptions)
      .pipe(
        catchError(err =>this.handleError(err))
  )}

  deleteCarRental(id: number) {
    return this.http.delete(this.baseUrl+id).pipe(
      catchError(err =>this.handleError(err))
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
