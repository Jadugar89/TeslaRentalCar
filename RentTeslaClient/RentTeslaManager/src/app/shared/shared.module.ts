import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarTypeService } from './cartype.service';
import { CarrentalService } from './carrental.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers:[CarTypeService,CarrentalService]
})
export class SharedModule { }
