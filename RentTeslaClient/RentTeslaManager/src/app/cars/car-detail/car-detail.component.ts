import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ICarDetail } from '../Interface';
import { CarService } from '../car.service';

@Component({
  selector: 'app-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.scss']
})
export class CarDetailComponent implements OnInit {
  carDetail={} as ICarDetail;
  carForm = this.fb.group({
    isPrepared: [false],
    isFree: [false],
    dailyPrice: [0, Validators.required],
    plates: ['', Validators.required],
    carTypeDto: this.fb.group({
      name: ['', Validators.required],
      motor: ['', Validators.required],
      range: [0, Validators.required],
      seats: [0, Validators.required]
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
              private fb: FormBuilder) {
                console.log("CarDetailComponent")
              }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.carService.getCar(+id).subscribe((car: ICarDetail) => {
        let carDetail = car;
        this.carDetail=car;
        console.log(carDetail);
       this.carForm.patchValue(
        {
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
        
      });
    }

  }


  onSubmit() {
    console.log(this.carForm.value);
    // Do something with the car object
  }
}
