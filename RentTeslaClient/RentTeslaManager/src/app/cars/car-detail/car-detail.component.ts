import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ICarDetail, ICarRental, ICarType } from '../../shared/models/interface';
import { CarService } from '../car.service';
import { CarTypeService } from '../../shared/services/cartype.service';
import { ToastrService } from 'ngx-toastr';
import { CarrentalService } from 'src/app/shared/services/carrental.service';

@Component({
  selector: 'app-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.scss']
})
export class CarDetailComponent implements OnInit  {
   car:ICarDetail ={
     id: 0,
     isPrepared: false,
     isFree: false,
     dailyPrice: 0,
     plates: '',
     carTypeDto: {
      id:0,
      name:'',
      motor:'',
      range:0,
      seats:0
     },
     carRentalDto: {
      id:0,
      name:'',
      street:'',
      postalCode:'',
      city:''
     }
   }
   carTypes:ICarType[]=[]
   carRentals:ICarRental[]=[]

  constructor(private route: ActivatedRoute, 
              private carService: CarService,
              private carTypesService: CarTypeService,
              private carRentalService: CarrentalService,
              private toastr: ToastrService,
              private router:Router) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.carService.getCar(+id).subscribe((car: ICarDetail) => {
        this.car=car;
        console.log(car);
      });

      this.carTypesService.getCarTypes().subscribe((carTypes:ICarType[])=>{
          this.carTypes=carTypes;
      })

      this.carRentalService.getCarRentals().subscribe((carRentals:ICarRental[])=>{
        this.carRentals=carRentals;
    })
    }
  }
  
  onSubmit(carDetail:ICarDetail) {
      this.carService.updateCarDetail(carDetail).subscribe(response => {
        this.toastr.success('Car details updated successfully', 'Update');
      }, error => {
        console.log('Error occurred while updating car details:', error);
        this.toastr.error('Error occurred while updating car details:', 'Update');
      });
    }
  
  onDelete(carId:number){
    this.carService.deleteCar(carId).subscribe(response => {
      this.toastr.success('Car deleted successfully', 'Delete');
      this.router.navigate(['/cars']);
    },error=>{
      console.log('Error occurred while delete car:', error);
      this.toastr.error('Error occurred while delete car:', 'delete');
    });
  }
}
