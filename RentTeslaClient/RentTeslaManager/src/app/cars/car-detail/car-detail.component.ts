import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ICarDetail, ICarType } from '../../shared/interface';
import { CarService } from '../car.service';
import { CarTypeService } from '../../shared/car-type.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.scss']
})
export class CarDetailComponent implements OnInit {
  carDetail={} as ICarDetail;
  carTypes: ICarType[]=[];
  carForm = this.fb.group({
    id: 0,
    isPrepared: [false],
    isFree: [false],
    dailyPrice: [0, Validators.required],
    plates: ['', Validators.required],
    carTypeDto: this.fb.group({
      name: [ '', Validators.required],
      motor:[{value: '', disabled:true}, Validators.required],
      range:[{ value: 0, disabled:true},  Validators.required],
      seats: [{ value: 0, disabled:true}, Validators.required]
    }),
    carRentalDto: this.fb.group({
      name: ['', Validators.required],
      city: ['', Validators.required],
      street: ['', Validators.required],
      postalCode: ['', Validators.required]
    })
  });
 

  constructor(private route: ActivatedRoute, 
              private carService: CarService,
              private carTypesService: CarTypeService,
              private toastr: ToastrService,
              private fb: FormBuilder) {
                console.log("CarDetailComponent")
              }
  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.carService.getCar(+id).subscribe((car: ICarDetail) => {
        this.UpdateForm(car);
      });
      this.carTypesService.getCarTypes().subscribe((carTypes:ICarType[])=>{
          this.carTypes=carTypes;
      })
    }

  }
  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

  onSubmit() {
    if (this.carForm.valid) {
      console.log(this.carForm.getRawValue() );
    this.carDetail = this.carForm.getRawValue() as ICarDetail;
    this.carService.updateCarDetail(this.carDetail).subscribe(response => {
      console.log(response);
      console.log('Car details updated successfully!');
      this.showSuccess();
    }, error => {
      console.log('Error occurred while updating car details:', error);
    });

    }
    
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
            name: carDetail.carTypeDto.name,
            motor: carDetail.carTypeDto.motor,
            range: carDetail.carTypeDto.range,
            seats: carDetail.carTypeDto.seats,
          },
          carRentalDto: {
            name: this.carDetail.carRentalDto.name,
            city: carDetail.carRentalDto.city,
            street: carDetail.carRentalDto.street,
            postalCode: carDetail.carRentalDto.postalCode,
          }
        })
  }
}
