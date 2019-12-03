import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchJobSeekerComponent } from './search-job-seeker.component';

describe('SearchJobSeekerComponent', () => {
  let component: SearchJobSeekerComponent;
  let fixture: ComponentFixture<SearchJobSeekerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchJobSeekerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchJobSeekerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
