/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProductmodelService } from './productmodel.service';

describe('Service: Productmodel', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProductmodelService]
    });
  });

  it('should ...', inject([ProductmodelService], (service: ProductmodelService) => {
    expect(service).toBeTruthy();
  }));
});
