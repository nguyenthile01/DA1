import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailJobseekerComponent } from './detail-jobseeker.component';

describe('DetailJobseekerComponent', () => {
  let component: DetailJobseekerComponent;
  let fixture: ComponentFixture<DetailJobseekerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailJobseekerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailJobseekerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
