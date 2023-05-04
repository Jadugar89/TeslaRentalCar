import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarRentalRoutingModule } from './car-rental-routing.module';
import { CarRentalComponent } from './car-rental/car-rental.component';
import { CarRentalListComponent } from './car-rental-list/car-rental-list.component';
import { SharedModule } from "../shared/shared.module";
import { NgxPaginationModule } from 'ngx-pagination';
import { CarRentalDetailComponent } from './car-rental-detail/car-rental-detail.component';
import { CarRentalService } from './car-rental.service';
import { CarRentalFormComponent } from './car-rental-form/car-rental-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddCarRentalComponent } from './add-car-rental/add-car-rental.component';



@NgModule({
    declarations: [
        CarRentalComponent,
        CarRentalListComponent,
        CarRentalDetailComponent,
        CarRentalFormComponent,
        AddCarRentalComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        ReactiveFormsModule,
        NgxPaginationModule,
        CarRentalRoutingModule,
    ],
    providers:[CarRentalService],
    exports:[]
})
export class CarRentalModule { }
