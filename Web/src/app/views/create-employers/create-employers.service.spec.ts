import { TestBed } from '@angular/core/testing';

import { CreateEmployersService } from './create-employers.service';

describe('CreateEmployersService', () => {
  let service: CreateEmployersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateEmployersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
