import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateJobSeekersComponent } from './create-job-seekers.component';

describe('CreateJobSeekersComponent', () => {
  let component: CreateJobSeekersComponent;
  let fixture: ComponentFixture<CreateJobSeekersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateJobSeekersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateJobSeekersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
