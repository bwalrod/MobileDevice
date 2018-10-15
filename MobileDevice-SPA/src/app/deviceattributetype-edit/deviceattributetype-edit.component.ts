import { Router, ActivatedRoute } from '@angular/router';
import { DeviceAttributeTypeService } from './../_services/deviceattributetype.service';
import { DeviceAttributeType } from '../_models/deviceattributetype';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-deviceattributetype-edit',
  templateUrl: './deviceattributetype-edit.component.html',
  styleUrls: ['./deviceattributetype-edit.component.css']
})
export class DeviceattributetypeEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: DeviceAttributeType;
  newElement: DeviceAttributeType = {
    id: 0,
    name: '',
    active: true,
    deviceAttributeCount: 0
  };
  elementLabel = 'Device Attribute Type';
  elementTypeLabel = 'device attribute type';
  elementRoute = 'deviceattributetypes';
  originalElement: DeviceAttributeType;

  constructor(private service: DeviceAttributeTypeService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      console.log(data['deviceattributetype']);
      if (data['deviceattributetype']) {
        this.element = data['deviceattributetype'];
        this.populateOriginal();
      }
    });
  }

  updateElement() {
    this.service.updateDeviceAttributeType(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
        this.populateOriginal();
      }, error => {
        this.alertify.error(error);
        this.editForm.reset(this.originalElement);
      });
  }

  insertElement() {
    this.service.addDeviceAttributeType(this.element)
      .subscribe((element: DeviceAttributeType) => {
        this.alertify.success(this.elementLabel + ' added successfully');
        this.element = element;
      }, error => {
        this.alertify.error(error);
        this.editForm.reset(this.originalElement);
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
      this.service.deactivateDeviceAttributeType(id)
        .subscribe(() => {
          this.router.navigate(['/deviceattributetypes']);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList(e: Event) {
    e.preventDefault();
    e.stopPropagation();
    this.router.navigate(['/deviceattributetypes']);
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.name = this.element.name;
    this.originalElement.active = this.element.active;
  }
}
