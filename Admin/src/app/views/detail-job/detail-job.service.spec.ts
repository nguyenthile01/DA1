import { TestBed } from '@angular/core/testing';

import { DetailJobService } from './detail-job.service';

describe('DetailJobService', () => {
  let service: DetailJobService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DetailJobService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
