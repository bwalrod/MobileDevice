/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AssigneeListComponent } from './assignee-list.component';

describe('AssigneeListComponent', () => {
  let component: AssigneeListComponent;
  let fixture: ComponentFixture<AssigneeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssigneeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssigneeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
