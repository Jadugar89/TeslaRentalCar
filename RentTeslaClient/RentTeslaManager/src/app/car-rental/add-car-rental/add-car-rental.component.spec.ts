import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCarRentalComponent } from './add-car-rental.component';

describe('AddCarRentalComponent', () => {
  let component: AddCarRentalComponent;
  let fixture: ComponentFixture<AddCarRentalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCarRentalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCarRentalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
