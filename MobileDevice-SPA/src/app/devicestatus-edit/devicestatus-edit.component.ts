import { NgForm } from '@angular/forms';
import { DeviceStatus } from './../_models/DeviceStatus';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DeviceStatusService } from '../_services/devicestatus.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-devicestatus-edit',
  templateUrl: './devicestatus-edit.component.html',
  styleUrls: ['./devicestatus-edit.component.css']
})
export class DevicestatusEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: DeviceStatus;
  newElement: DeviceStatus = {
    id: 0,
    name: '',
    active: true,
    deviceCount: 0
  };
  elementLabel = 'Device Status';
  elementTypeLabel = 'device status';
  elementRoute = 'devicestatuses';
  originalElement: DeviceStatus;


  formInvalid = true;


  constructor(private service: DeviceStatusService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['devicestatus']) {
        this.element = data['devicestatus'];
        this.isFormValid();
        this.populateOriginal();
      }
    });
  }

  updateElement() {
    this.service.updateDeviceStatus(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
      });
  }

  insertElement() {
    this.service.addDeviceStatus(this.element)
      .subscribe((element: DeviceStatus) => {
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
      this.service.deactivateDeviceStatus(id)
        .subscribe(() => {
          this.router.navigate(['/devicestatuses']);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList(e: Event) {
    e.preventDefault();
    e.stopPropagation();
    this.router.navigate(['/devicestatuses']);
  }

  isFormValid() {
    this.formInvalid = false;
    if (this.element.name === '') {
      this.formInvalid = true;
    }
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.name = this.element.name;
    this.originalElement.active = this.element.active;
  }
}
