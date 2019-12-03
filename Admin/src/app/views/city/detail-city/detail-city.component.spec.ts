import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailCityComponent } from './detail-city.component';

describe('DetailCityComponent', () => {
  let component: DetailCityComponent;
  let fixture: ComponentFixture<DetailCityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailCityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailCityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
