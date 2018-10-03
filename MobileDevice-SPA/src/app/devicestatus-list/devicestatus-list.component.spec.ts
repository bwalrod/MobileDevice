/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DevicestatusListComponent } from './devicestatus-list.component';

describe('DevicestatusListComponent', () => {
  let component: DevicestatusListComponent;
  let fixture: ComponentFixture<DevicestatusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevicestatusListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevicestatusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
