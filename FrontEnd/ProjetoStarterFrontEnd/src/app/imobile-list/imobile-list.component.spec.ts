import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImobileListComponent } from './imobile-list.component';

describe('ImobileListComponent', () => {
  let component: ImobileListComponent;
  let fixture: ComponentFixture<ImobileListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImobileListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImobileListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
