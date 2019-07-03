import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RealtiesComponent } from './realties.component';

describe('RealtiesComponent', () => {
  let component: RealtiesComponent;
  let fixture: ComponentFixture<RealtiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RealtiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RealtiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
