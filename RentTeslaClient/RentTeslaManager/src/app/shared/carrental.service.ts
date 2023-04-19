import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICarRental } from './interface';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarrentalService {

  baseUrl: string = 'https://localhost:7236/api/CarRental/';

  constructor(private http: HttpClient) { }


  getCarRentals() : Observable<ICarRental[]> {
    return this.http.get<ICarRental[]>(this.baseUrl)
        .pipe(
          catchError(err => {
            console.log('Handling error locally and rethrowing it...', err);
            return throwError(err);
        })
      )}

}
