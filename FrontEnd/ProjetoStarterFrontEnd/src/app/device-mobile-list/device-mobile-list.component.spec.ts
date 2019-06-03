import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceMobileListComponent } from './device-mobile-list.component';

describe('DeviceMobileListComponent', () => {
  let component: DeviceMobileListComponent;
  let fixture: ComponentFixture<DeviceMobileListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeviceMobileListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceMobileListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
