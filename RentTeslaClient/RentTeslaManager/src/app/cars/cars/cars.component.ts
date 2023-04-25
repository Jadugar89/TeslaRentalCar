import { Component, OnInit } from '@angular/core';
import { CarService } from '../car.service';
import { ICar } from '../../shared/models/interface';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.scss']
})
export class CarsComponent implements OnInit {
  cars: ICar[]=[];
  filterCars:ICar[]=[];

  constructor(private carService: CarService) { }

  ngOnInit(): void {
   this.carService.getCars().subscribe(
    (cars: ICar[]) => {
      this.cars = cars
      this.filterCars= cars}
    );
  }

  OnChange(seachText:string)
  {
    
    this.filterCars = this.funcfilterCars(this.cars, seachText);
  }

  funcfilterCars(cars: ICar[], seachText: string): ICar[] {
    if (!seachText) return cars; // if no car type is selected, return all cars
    return cars.filter(car => 
      car.plates.toLocaleLowerCase().includes(seachText.toLocaleLowerCase()) ||
      car.name.toLocaleLowerCase().includes(seachText.toLocaleLowerCase())||
      car.carRentalName.toLocaleLowerCase().includes(seachText.toLocaleLowerCase())||
      car.carRentalCity.toLocaleLowerCase().includes(seachText.toLocaleLowerCase())
    );
  }
}
