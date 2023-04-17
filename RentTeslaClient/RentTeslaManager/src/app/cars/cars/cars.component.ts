import { Component, OnInit } from '@angular/core';
import { CarService } from '../car.service';
import { ICar } from '../../shared/interface';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.scss']
})
export class CarsComponent implements OnInit {
  cars: ICar[]=[];

  constructor(private carService: CarService) { }

  ngOnInit(): void {
   this.carService.getCars().subscribe(
    (cars: ICar[]) => this.cars = cars);
  }

}
