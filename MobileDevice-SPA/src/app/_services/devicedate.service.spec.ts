/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DevicedateService } from './devicedate.service';

describe('Service: Devicedate', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DevicedateService]
    });
  });

  it('should ...', inject([DevicedateService], (service: DevicedateService) => {
    expect(service).toBeTruthy();
  }));
});
