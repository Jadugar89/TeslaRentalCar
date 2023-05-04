import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarRentalDetailComponent } from './car-rental-detail.component';

describe('CarRentalDetailComponent', () => {
  let component: CarRentalDetailComponent;
  let fixture: ComponentFixture<CarRentalDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarRentalDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarRentalDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
