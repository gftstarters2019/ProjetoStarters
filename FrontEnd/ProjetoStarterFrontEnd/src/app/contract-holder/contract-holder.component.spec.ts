import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractHolderComponent } from './contract-holder.component';

describe('ContractHolderComponent', () => {
  let component: ContractHolderComponent;
  let fixture: ComponentFixture<ContractHolderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractHolderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractHolderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
