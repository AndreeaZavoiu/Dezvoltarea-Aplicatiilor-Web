import { TestBed } from '@angular/core/testing';

import { CursService } from './curs.service';

describe('CursService', () => {
  let service: CursService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CursService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
