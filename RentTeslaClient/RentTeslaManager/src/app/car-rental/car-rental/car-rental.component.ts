import { Component, OnInit } from '@angular/core';
import { ICarRental } from 'src/app/shared/models/interface';
import { CarRentalService } from '../car-rental.service';


@Component({
  selector: 'app-car-rental',
  templateUrl: './car-rental.component.html',
  styleUrls: ['./car-rental.component.scss']
})
export class CarRentalComponent implements OnInit {
  carRentals: ICarRental[]=[];
  filterCarRentals:ICarRental[]=[];

  constructor(private carRentalService:CarRentalService) { }

  ngOnInit(): void {
    this.carRentalService.getCarRentals().subscribe(
     (cars: ICarRental[]) => {
       this.carRentals = cars
       this.filterCarRentals= cars}
     );
   }

   OnChange(seachText:string)
  {
    
    this.filterCarRentals = this.funcfilterCars(this.carRentals, seachText);
  }

  funcfilterCars(cars: ICarRental[], seachText: string): ICarRental[] {
    if (!seachText) return this.carRentals; // if no car type is selected, return all cars
    return cars.filter(car => 
      car.name.toLocaleLowerCase().includes(seachText.toLocaleLowerCase()) ||
      car.city.toLocaleLowerCase().includes(seachText.toLocaleLowerCase())
    );
  }
}
