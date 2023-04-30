import { Component, OnInit } from '@angular/core';
import { ICarDetail,ICarRental, ICarType } from 'src/app/shared/models/interface';
import { CarService } from '../car.service';
import { CarTypeService } from 'src/app/shared/services/cartype.service';
import { CarrentalService } from 'src/app/shared/services/carrental.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ICreatedCar } from '../models/create-car.model';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.scss']
})
export class AddCarComponent implements OnInit {
  car:ICarDetail= {} as ICarDetail;
  carTypes:ICarType[]=[]
  carRentals:ICarRental[]=[]

  constructor(private carService: CarService,
    private carTypesService: CarTypeService,
    private carRentalService: CarrentalService,
    private toastr: ToastrService,
    private router:Router){}

    ngOnInit(): void {
      this.carTypesService.getCarTypes().subscribe((carTypes:ICarType[])=>{
        this.carTypes=carTypes;
      })
      this.carRentalService.getCarRentals().subscribe((carRentals:ICarRental[])=>{
        this.carRentals=carRentals;
      })
    }

  onSubmit(carDetail:ICreatedCar) {
    this.carService.createCarDetail(carDetail).subscribe(response => {
      this.toastr.success('Car created successfully', 'Created');
      this.router.navigate(['/cars']);
    }, error => {
      console.log('Error occurred while creating car:', error);
      this.toastr.error('Error occurred while creating car', 'Update');
    });
  }
}

