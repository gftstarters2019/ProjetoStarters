import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileDeviceListComponent } from './mobile-device-list.component';

describe('MobileDeviceListComponent', () => {
  let component: MobileDeviceListComponent;
  let fixture: ComponentFixture<MobileDeviceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MobileDeviceListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MobileDeviceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
