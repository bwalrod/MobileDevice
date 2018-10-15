/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SimcardEditComponent } from './simcard-edit.component';

describe('SimcardEditComponent', () => {
  let component: SimcardEditComponent;
  let fixture: ComponentFixture<SimcardEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SimcardEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SimcardEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
