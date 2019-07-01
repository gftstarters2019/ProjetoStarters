import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionButtonBeneficiariesComponent } from './action-button-beneficiaries.component';

describe('ActionButtonBeneficiariesComponent', () => {
  let component: ActionButtonBeneficiariesComponent;
  let fixture: ComponentFixture<ActionButtonBeneficiariesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActionButtonBeneficiariesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActionButtonBeneficiariesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
