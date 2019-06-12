import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RealtiesListComponent } from './realties-list.component';

describe('RealtiesListComponent', () => {
  let component: RealtiesListComponent;
  let fixture: ComponentFixture<RealtiesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RealtiesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RealtiesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
