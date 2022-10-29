import { TestBed } from '@angular/core/testing';

import { ProgramItemsService } from './program-items.service';

describe('ProgramItemsService', () => {
  let service: ProgramItemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProgramItemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
