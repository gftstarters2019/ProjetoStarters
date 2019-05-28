import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiaryVehicleComponent } from './beneficiary-vehicle.component';

describe('BeneficiaryVehicleComponent', () => {
  let component: BeneficiaryVehicleComponent;
  let fixture: ComponentFixture<BeneficiaryVehicleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiaryVehicleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiaryVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
