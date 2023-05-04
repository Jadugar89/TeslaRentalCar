import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { ICarRental } from 'src/app/shared/models/interface';

@Component({
  selector: 'app-car-rental-form',
  templateUrl: './car-rental-form.component.html',
  styleUrls: ['./car-rental-form.component.scss']
})

export class CarRentalFormComponent  implements OnChanges{
  @Input() carRental={} as ICarRental;
  @Input() daleteBtnShow:boolean=false;
  @Output() carDeleteEvent = new EventEmitter<number>();
  @Output() submitEvent = new EventEmitter<ICarRental>();

  carRentalForm= this.fb.group({
    id: [this.carRental?.id],      
    name: [this.carRental?.name, Validators.required],
    city: [this.carRental?.city, Validators.required],
    street: [this.carRental?.street, Validators.required],
    postalCode: [this.carRental?.postalCode, Validators.required]
  });

  constructor(private fb:FormBuilder){

  }
  
  ngOnChanges(changes: SimpleChanges): void {
    const carChange = changes['carRental'];
   if (carChange) {
     console.log(changes);
     this.carRentalForm.patchValue(this.carRental);
   }
 }

 OnDelete(){
   const carDetailId = this.carRentalForm.value.id;
   if(carDetailId)
   {
     this.carDeleteEvent.emit(carDetailId);
   }
 }

 onSubmit() {
   if (this.carRentalForm.valid) {
     const carDetail = this.carRentalForm.getRawValue() as ICarRental;
     if(carDetail)
     {
       this.submitEvent.emit(carDetail);
     }
   }
 }
}
