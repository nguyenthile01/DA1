import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployersComponent } from './create-employers.component';

describe('CreateEmployersComponent', () => {
  let component: CreateEmployersComponent;
  let fixture: ComponentFixture<CreateEmployersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
