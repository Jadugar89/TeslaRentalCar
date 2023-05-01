import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarsComponent } from './cars/cars.component';
import { CarDetailComponent } from './car-detail/car-detail.component';
import { AddCarComponent } from './add-car/add-car.component';
import { AuthenticatedGuard } from '../core/guard/authenticated.guard';

const routes: Routes = [
  { path: 'cars',pathMatch: 'full',  component: CarsComponent,canActivate: [AuthenticatedGuard] },
  { path: 'cars/:id',pathMatch: 'full', component: CarDetailComponent,canActivate: [AuthenticatedGuard] },
  { path: 'addcar',pathMatch: 'full', component: AddCarComponent,canActivate: [AuthenticatedGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarsRoutingModule { }
