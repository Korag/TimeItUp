import { TestBed } from '@angular/core/testing';

import { NonAuthUserGuard } from './non-auth-user.guard';

describe('NonAuthUserGuard', () => {
  let guard: NonAuthUserGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(NonAuthUserGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
