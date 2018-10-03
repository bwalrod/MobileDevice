/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProducttypeService } from './producttype.service';

describe('Service: Producttype', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProducttypeService]
    });
  });

  it('should ...', inject([ProducttypeService], (service: ProducttypeService) => {
    expect(service).toBeTruthy();
  }));
});
