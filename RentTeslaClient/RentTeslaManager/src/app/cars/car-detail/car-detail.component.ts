import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ICarDetail, ICarRental, ICarType } from '../../shared/interface';
import { CarService } from '../car.service';
import { CarTypeService } from '../../shared/cartype.service';
import { ToastrService } from 'ngx-toastr';
import { CarrentalService } from 'src/app/shared/carrental.service';

@Component({
  selector: 'app-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.scss']
})
export class CarDetailComponent implements OnInit {
  carDetail={} as ICarDetail;
  carTypes: ICarType[]=[];
  carRentals: ICarRental[]=[];

  carForm = this.fb.group({
    id: 0,
    isPrepared: [false],
    isFree: [false],
    dailyPrice: [0, Validators.required],
    plates: ['', Validators.required],
    carTypeDto: this.fb.group({
      id: 0,
      name: [ '', Validators.required],
      motor:[{value: '', disabled:true}, Validators.required],
      range:[{ value: 0, disabled:true},  Validators.required],
      seats: [{ value: 0, disabled:true}, Validators.required]
    }),
    carRentalDto: this.fb.group({
      id: 0,
      name: ['', Validators.required],
      city: [{value: '', disabled:true}, Validators.required],
      street: [{value: '', disabled:true}, Validators.required],
      postalCode: [{value: '', disabled:true}, Validators.required]
    })
  });
 

  constructor(private route: ActivatedRoute, 
              private carService: CarService,
              private carTypesService: CarTypeService,
              private carRentalService: CarrentalService,
              private toastr: ToastrService,
              private router:Router,
              private fb: FormBuilder) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.carService.getCar(+id).subscribe((car: ICarDetail) => {
        this.UpdateForm(car);
      });

      this.carTypesService.getCarTypes().subscribe((carTypes:ICarType[])=>{
          this.carTypes=carTypes;
      })

      this.carRentalService.getCarRentals().subscribe((carRentals:ICarRental[])=>{
        this.carRentals=carRentals;
    })
    }
  }

  onSubmit() {
    if (this.carForm.valid) {
      this.carDetail = this.carForm.getRawValue() as ICarDetail;
      this.carService.updateCarDetail(this.carDetail).subscribe(response => {
        console.log(response);
        console.log('Car details updated successfully!');
        this.toastr.success('Car details updated successfully', 'Update');
      }, error => {
        console.log('Error occurred while updating car details:', error);
        this.toastr.error('Error occurred while updating car details:', 'Update');
      });
    }
  }
  
  onDelete(){
    this.carService.deleteCar(this.carDetail.id).subscribe(response => {
      this.toastr.success('Car deleted successfully', 'Delete');
      this.router.navigate(['/cars']);
    },error=>{
      console.log('Error occurred while delete car:', error);
      this.toastr.error('Error occurred while delete car:', 'delete');
    });
  }

  onCarTypeChange(newValue:any)
  {
    if(newValue.target.value)
    {
      const selectedCarType = this.carTypes.find(carType => carType.name === newValue.target.value);
      if(selectedCarType)
      {
        this.carForm.patchValue({
          carTypeDto: {
            motor: selectedCarType.motor,
            range: selectedCarType.range,
            seats: selectedCarType.seats,
          },
        })
      }
    }
  }

  onCarRentalChange(newValue:any)
  {
    if(newValue.target.value)
    {
      const selectedCarRental = this.carRentals.find(carRental => carRental.name === newValue.target.value);
      if(selectedCarRental)
      {
        this.carForm.patchValue({
          carRentalDto: {
            city: selectedCarRental.city,
            street: selectedCarRental.street,
            postalCode: selectedCarRental.postalCode,
          },
        })
      }
    }
  }

  private UpdateForm(car:ICarDetail)
  {
    let carDetail = car;
        this.carDetail=car;
        console.log(carDetail);
        this.carForm.setValue(
        {
          id: carDetail.id,
          isPrepared: carDetail.isPrepared,
          isFree: carDetail.isFree,
          dailyPrice: carDetail.dailyPrice ,
          plates: carDetail.plates,
          carTypeDto: {
            id:carDetail.carTypeDto.id,
            name: carDetail.carTypeDto.name,
            motor: carDetail.carTypeDto.motor,
            range: carDetail.carTypeDto.range,
            seats: carDetail.carTypeDto.seats,
          },
          carRentalDto: {
            id:this.carDetail.carRentalDto.id,
            name: this.carDetail.carRentalDto.name,
            city: carDetail.carRentalDto.city,
            street: carDetail.carRentalDto.street,
            postalCode: carDetail.carRentalDto.postalCode,
          }
        })
  }
}
