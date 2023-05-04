import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarRentalListComponent } from './car-rental-list.component';

describe('CarRentalListComponent', () => {
  let component: CarRentalListComponent;
  let fixture: ComponentFixture<CarRentalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarRentalListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarRentalListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
