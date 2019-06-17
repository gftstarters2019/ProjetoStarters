import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiarylistAddComponent } from './beneficiarylist-add.component';

describe('BeneficiarylistAddComponent', () => {
  let component: BeneficiarylistAddComponent;
  let fixture: ComponentFixture<BeneficiarylistAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiarylistAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiarylistAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
