import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ICarRental } from 'src/app/shared/models/interface';

@Component({
  selector: 'app-car-rental-list',
  templateUrl: './car-rental-list.component.html',
  styleUrls: ['./car-rental-list.component.scss']
})
export class CarRentalListComponent {
  @Input() carRentals: ICarRental[]=[];
  // Pagination parameters.
  p: number = 1;
   count: number = 15;

  constructor(private router: Router) { }

  onSelect(carRental: ICarRental) {
    this.router.navigate(['/rentalcars', carRental.id]);
  }

  trackByFn(index:number,carRental:ICarRental) {    
    return carRental.id; 
 }
}
