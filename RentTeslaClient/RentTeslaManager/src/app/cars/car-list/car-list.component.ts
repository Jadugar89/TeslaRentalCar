import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICar } from '../Interface';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.scss']
})
export class CarListComponent implements OnInit {
  @Input() cars: ICar[]=[];
 // Pagination parameters.
 p: number = 1;
 count: number = 15;

  constructor(private router: Router) { }

  ngOnInit() {
 
  }

  onSelect(car: ICar) {
    this.router.navigate(['/cars', car.id]);
  }
}
