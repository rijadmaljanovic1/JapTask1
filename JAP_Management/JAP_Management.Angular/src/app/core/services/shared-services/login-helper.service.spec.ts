import { TestBed } from '@angular/core/testing';

import { LoginHelperService } from './login-helper.service';

describe('LoginHelperService', () => {
  let service: LoginHelperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginHelperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
