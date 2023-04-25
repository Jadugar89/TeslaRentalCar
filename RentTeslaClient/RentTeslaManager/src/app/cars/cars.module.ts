import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxPaginationModule } from 'ngx-pagination';


import { CarListComponent } from './car-list/car-list.component';
import { CarDetailComponent } from './car-detail/car-detail.component';
import { CarsComponent } from './cars/cars.component';
import { CarService } from './car.service';
import { CarFormComponent } from './car-form/car-form.component'
import { CarsRoutingModule } from './cars-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AddCarComponent } from './add-car/add-car.component';




@NgModule({
    declarations: [
        CarListComponent,
        CarDetailComponent,
        CarsComponent,
        CarFormComponent,
        AddCarComponent
    ],
    exports: [CarsComponent],
    providers: [CarService],
    imports: [
        CommonModule,
        SharedModule,
        NgxPaginationModule,
        ReactiveFormsModule,
        CarsRoutingModule,
    ]
})
export class CarsModule { }
