import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';

import { ICarDetail, ICarRental, ICarType } from 'src/app/shared/models/interface';

@Component({
  selector: 'app-car-form',
  templateUrl: './car-form.component.html',
  styleUrls: ['./car-form.component.scss']
})
export class CarFormComponent implements OnChanges{
  @Input() carDetail={} as ICarDetail;
  @Input() carTypes: ICarType[]=[];
  @Input() carRentals: ICarRental[]=[];
  @Input() daleteBtnShow:boolean=false;
  @Output() carDeleteEvent = new EventEmitter<number>();
  @Output() submitEvent = new EventEmitter<ICarDetail>();


  carForm = this.fb.group({
    id: [this.carDetail?.id],
    isPrepared: [this.carDetail?.isPrepared],
    isFree: [this.carDetail?.isFree],
    dailyPrice: [this.carDetail?.dailyPrice, Validators.required],
    plates: [this.carDetail?.plates, Validators.required],
    carTypeDto: this.fb.group({
      id: this.carDetail?.carTypeDto?.id,
      name: [ this.carDetail?.carTypeDto?.name, Validators.required],
      motor:[{value: this.carDetail?.carTypeDto?.motor, disabled:true}, Validators.required],
      range:[{ value: this.carDetail?.carTypeDto?.range, disabled:true},  Validators.required],
      seats: [{ value: this.carDetail?.carTypeDto?.seats, disabled:true}, Validators.required]
    }),
    carRentalDto: this.fb.group({
      id: [this.carDetail?.carRentalDto?.id],
      name: [this.carDetail?.carRentalDto?.name, Validators.required],
      city: [{value: this.carDetail?.carRentalDto?.city, disabled:true}, Validators.required],
      street: [{value:this.carDetail?.carRentalDto?.street, disabled:true}, Validators.required],
      postalCode: [{value: this.carDetail?.carRentalDto?.postalCode, disabled:true}, Validators.required]
    })
  });

  constructor(private fb: FormBuilder){

  }

  ngOnChanges(changes: SimpleChanges): void {
     const carChange = changes['carDetail'];
    if (carChange) {
      console.log(changes);
      this.carForm.patchValue(this.carDetail);
    }
  }

  OnDelete(){
    const carDetailId = this.carForm.value.id;
    if(carDetailId)
    {
      this.carDeleteEvent.emit(carDetailId);
    }
  }

  onSubmit() {
    if (this.carForm.valid) {
      const carDetail = this.carForm.getRawValue() as ICarDetail;
      if(carDetail)
      {
        this.submitEvent.emit(carDetail);
      }
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
          carTypeDto: selectedCarType
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
          carRentalDto: selectedCarRental
        })
      }
    }
  }
  
}
