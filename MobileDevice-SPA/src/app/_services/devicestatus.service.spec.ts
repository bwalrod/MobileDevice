/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DevicestatusService } from './devicestatus.service';

describe('Service: Devicestatus', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DevicestatusService]
    });
  });

  it('should ...', inject([DevicestatusService], (service: DevicestatusService) => {
    expect(service).toBeTruthy();
  }));
});
