import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarRentalComponent } from './car-rental/car-rental.component';
import { CarRentalDetailComponent } from './car-rental-detail/car-rental-detail.component';
import { AuthenticatedGuard } from '../core/guard/authenticated.guard';
import { AddCarRentalComponent } from './add-car-rental/add-car-rental.component';

const routes: Routes = [
  { path: 'rentalcars',pathMatch:'full', component: CarRentalComponent,canActivate: [AuthenticatedGuard]},
  { path: 'rentalcars/:id',pathMatch:'full', component: CarRentalDetailComponent,canActivate: [AuthenticatedGuard]},
  { path: 'addrentalcar',pathMatch: 'full', component: AddCarRentalComponent,canActivate: [AuthenticatedGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarRentalRoutingModule { }
