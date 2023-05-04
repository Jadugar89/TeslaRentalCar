import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';

import {CarRentalService} from '../car-rental.service'
import { ICarRental } from 'src/app/shared/models/interface';

@Component({
  selector: 'app-car-rental-detail',
  templateUrl: './car-rental-detail.component.html',
  styleUrls: ['./car-rental-detail.component.scss']
})
export class CarRentalDetailComponent {
  carRental ={} as ICarRental ;

  constructor(private route:ActivatedRoute,
              private carRentalService:CarRentalService,
              private toastr: ToastrService,
              private router:Router ){}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.carRentalService.getCarRentalById(+id).subscribe((carRental:ICarRental)=>{
      this.carRental=carRental;
    })}
  }

  onSubmit(carDetail:ICarRental) {
    this.carRentalService.updateCarRental(carDetail).subscribe((response) => {
      this.toastr.success('Car details updated successfully', 'Update');
    },error => {
      console.log('Error occurred while updating car details:', error);
      this.toastr.error('Error occurred while updating car details:', 'Update');
    });
  }

  onDelete(carId:number){
    this.carRentalService.deleteCarRental(carId).subscribe(response => {
      this.toastr.success('Car deleted successfully', 'Delete');
      this.router.navigate(['/rentalcars']);
    },error=>{
      console.log('Error occurred while delete car:', error);
      this.toastr.error('Error occurred while delete car:', 'delete');
    });
}
}
