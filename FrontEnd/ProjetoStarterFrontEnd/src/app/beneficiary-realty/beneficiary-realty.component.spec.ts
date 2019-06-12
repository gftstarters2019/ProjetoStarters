import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiaryRealtyComponent } from './beneficiary-realty.component';

describe('BeneficiaryRealtyComponent', () => {
  let component: BeneficiaryRealtyComponent;
  let fixture: ComponentFixture<BeneficiaryRealtyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiaryRealtyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiaryRealtyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
