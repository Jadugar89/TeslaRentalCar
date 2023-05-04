import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ICar } from '../../shared/models/interface';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.scss']
})
export class CarListComponent  {
  @Input() cars: ICar[]=[];
  // Pagination parameters.
  p: number = 1;
   count: number = 15;

  constructor(private router: Router) { }

  onSelect(car: ICar) {
    this.router.navigate(['/cars', car.id]);
  }

  trackByFn(index:number,car:ICar) {    
    return car.id; 
 }
}
