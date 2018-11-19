/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DevicedateEditableRowComponent } from './devicedate-editable-row.component';

describe('DevicedateEditableRowComponent', () => {
  let component: DevicedateEditableRowComponent;
  let fixture: ComponentFixture<DevicedateEditableRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevicedateEditableRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevicedateEditableRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
