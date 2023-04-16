import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarRentalRoutingModule } from './car-rental-routing.module';
import { CarRentalComponent } from './car-rental/car-rental.component';



@NgModule({
  declarations: [
    CarRentalComponent
  ],
  imports: [
    CommonModule,
    CarRentalRoutingModule,
  ]
})
export class CarRentalModule { }
