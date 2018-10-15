/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SimcardService } from './simcard.service';

describe('Service: Simcard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SimcardService]
    });
  });

  it('should ...', inject([SimcardService], (service: SimcardService) => {
    expect(service).toBeTruthy();
  }));
});
