import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiaryIndividualComponent } from './beneficiary-individual.component';

describe('BeneficiaryIndividualComponent', () => {
  let component: BeneficiaryIndividualComponent;
  let fixture: ComponentFixture<BeneficiaryIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiaryIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiaryIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
