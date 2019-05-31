import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidatorCpfComponent } from './validator-cpf.component';

describe('ValidatorCpfComponent', () => {
  let component: ValidatorCpfComponent;
  let fixture: ComponentFixture<ValidatorCpfComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidatorCpfComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidatorCpfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
