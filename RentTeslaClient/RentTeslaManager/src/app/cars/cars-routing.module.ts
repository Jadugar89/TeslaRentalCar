import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarsComponent } from './cars/cars.component';
import { CarDetailComponent } from './car-detail/car-detail.component';
import { AddCarComponent } from './add-car/add-car.component';

const routes: Routes = [
  { path: 'cars',pathMatch: 'full',  component: CarsComponent},
  { path: 'cars/:id',pathMatch: 'full', component: CarDetailComponent },
  { path: 'addcar',pathMatch: 'full', component: AddCarComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarsRoutingModule { }
