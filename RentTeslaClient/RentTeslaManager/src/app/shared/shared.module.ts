import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarTypeService } from './car-type.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers:[CarTypeService]
})
export class SharedModule { }
