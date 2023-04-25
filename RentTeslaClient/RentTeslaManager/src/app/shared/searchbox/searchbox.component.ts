import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-searchbox',
  templateUrl: './searchbox.component.html',
  styleUrls: ['./searchbox.component.scss']
})
export class SearchboxComponent {
  @Output() searchString = new EventEmitter<string>();

  OnChange(event:any){
    this.searchString.emit(event.target.value);
  }
  
}
