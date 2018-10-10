/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProductcapacityService } from './productcapacity.service';

describe('Service: Productcapacity', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProductcapacityService]
    });
  });

  it('should ...', inject([ProductcapacityService], (service: ProductcapacityService) => {
    expect(service).toBeTruthy();
  }));
});
