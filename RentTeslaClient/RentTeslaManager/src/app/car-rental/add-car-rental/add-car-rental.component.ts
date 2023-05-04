import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CarRentalService } from '../car-rental.service';
import { ICreatedCarRental } from '../models/created-car-rental.model';
import { ICarRental } from 'src/app/shared/models/interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car-rental',
  templateUrl: './add-car-rental.component.html',
  styleUrls: ['./add-car-rental.component.scss']
})
export class AddCarRentalComponent {
  carRental= {} as ICarRental;

  constructor(private carRentalService:CarRentalService,
              private toastr: ToastrService,
              private router:Router){}

  onSubmit(createCarRental:ICreatedCarRental) {
    console.log(createCarRental)
    this.carRentalService.createCarRental(createCarRental).subscribe(response => {
      this.toastr.success('Car created successfully', 'Created');
      this.router.navigate(['/rentalcars']);
    }, error => {
      console.log('Error occurred while creating car:', error);
      this.toastr.error('Error occurred while creating car', 'Update');
    });
  }
}
