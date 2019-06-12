import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiaryMobileDeviceComponent } from './beneficiary-mobile-device.component';

describe('BeneficiaryMobileDeviceComponent', () => {
  let component: BeneficiaryMobileDeviceComponent;
  let fixture: ComponentFixture<BeneficiaryMobileDeviceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiaryMobileDeviceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiaryMobileDeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
