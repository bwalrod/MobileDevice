/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SimcardListComponent } from './simcard-list.component';

describe('SimcardListComponent', () => {
  let component: SimcardListComponent;
  let fixture: ComponentFixture<SimcardListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SimcardListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SimcardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
