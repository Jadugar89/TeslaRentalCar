import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarsComponent } from './cars/cars.component';
import { CarDetailComponent } from './car-detail/car-detail.component';

const routes: Routes = [
  { path: 'cars',pathMatch: 'full',  component: CarsComponent},
  { path: 'cars/:id',pathMatch: 'full', component: CarDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarsRoutingModule { }
