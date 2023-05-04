import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarTypeService } from './services/cartype.service';
import { SearchboxComponent } from './searchbox/searchbox.component';



@NgModule({
  declarations: [
    SearchboxComponent
  ],
  imports: [
    CommonModule
  ],
  providers:[CarTypeService],
  exports:[SearchboxComponent]
})
export class SharedModule { }
