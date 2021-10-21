import { TestBed } from '@angular/core/testing';

import { ValidationErrorPopulatorService } from './validation-error-populator.service';

describe('ValidationErrorPopulatorService', () => {
  let service: ValidationErrorPopulatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ValidationErrorPopulatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
