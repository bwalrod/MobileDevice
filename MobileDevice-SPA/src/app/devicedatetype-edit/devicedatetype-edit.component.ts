import { AlertifyService } from './../_services/alertify.service';
import { DeviceDateTypeService } from './../_services/devicedatetype.service';
import { DeviceDateType } from './../_models/devicedatetype';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-devicedatetype-edit',
  templateUrl: './devicedatetype-edit.component.html',
  styleUrls: ['./devicedatetype-edit.component.css']
})
export class DevicedatetypeEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: DeviceDateType;
  newElement: DeviceDateType = {
    id: 0,
    name: '',
    active: true,
    deviceDateCount: 0
  };
  originalElement: DeviceDateType;
  elementLabel = 'Device Date Type';
  elementTypeLabel = 'device date type';
  elementRoute = 'devicedatetypes';

  constructor(private service: DeviceDateTypeService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['devicedatetype']) {
        this.element = data['devicedatetype'];
        this.updateOriginalElement();
      }
    });
  }

  updateElement() {
    this.service.updateDeviceDateType(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
        this.updateOriginalElement();
      }, error => {
        this.alertify.error(error);
        this.editForm.reset(this.originalElement);
      });
  }

  insertElement() {
    this.service.addDeviceDateType(this.element)
      .subscribe((element: DeviceDateType) => {
        this.alertify.success(this.elementLabel + ' added successfully');
        this.element = element;
      }, error => {
        this.alertify.error(error);
      });
  }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.element.id > 0) {
        this.updateElement();
      } else {
        this.insertElement();
      }
    }
  }

  deactivateElement(id: number, e: Event) {
    e.preventDefault();
    e.stopPropagation();
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementTypeLabel + '?', () => {
      this.service.deactivateDeviceDateType(id)
        .subscribe(() => {
          this.router.navigate(['/' + this.elementRoute]);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList(e: Event) {
    e.preventDefault();
    e.stopPropagation();
    this.router.navigate(['/' + this.elementRoute]);
  }

  updateOriginalElement() {
    this.originalElement.id = this.element.id;
    this.originalElement.name = this.element.name;
    this.originalElement.active = this.element.active;
  }
}
