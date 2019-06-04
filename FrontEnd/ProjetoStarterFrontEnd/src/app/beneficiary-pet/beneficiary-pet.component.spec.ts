import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiaryPetComponent } from './beneficiary-pet.component';

describe('BeneficiaryPetComponent', () => {
  let component: BeneficiaryPetComponent;
  let fixture: ComponentFixture<BeneficiaryPetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiaryPetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiaryPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
