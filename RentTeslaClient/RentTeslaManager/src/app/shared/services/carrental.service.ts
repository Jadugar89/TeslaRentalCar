import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICarRental } from '../models/interface';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarrentalService {

  baseUrl: string = environment.baseUrl+'/api/CarRental/';

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
