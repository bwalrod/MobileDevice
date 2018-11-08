import { Router, ActivatedRoute } from '@angular/router';
import { DeviceService } from './../_services/device.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Device } from '../_models/device';
import { Product } from '../_models/product';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-device-edit',
  templateUrl: './device-edit.component.html',
  styleUrls: ['./device-edit.component.css']
})
export class DeviceEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: Device;
  newElement: Device = {
    id: 0,
    serialNumber: '',
    esn: '',
    os: '',
    productId: 0,
    partNum: '',
    assignmentType: 0,
    deviceStatusId: 0,
    deviceStatusName: '',
    assigneeId: 0,
    assigneeFirstName: '',
    assigneeLastName: '',
    assigneeDepartmentName: '',
    productModelId: 0,
    productModelName: '',
    productCapacityName: '',
    productManufacturerName: '',
    productTypeId: 0,
    productTypeName: '',
    simId: 0,
    simICCId: '',
    simCarrier: '',
    simPhoneNumber: '',
    active: true
  };

  elementLabel = 'Device';
  elementTypeLabel = 'device';
  elementRoute = 'devices';
  originalElement: Device;
  formInvalid = false;

  constructor(private service: DeviceService, private router: Router,
              private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['device']) {
        this.element = data['device'];
        this.populateOriginal();
        console.log('OnInit');
      }
    });
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.serialNumber = this.element.serialNumber;
    this.originalElement.esn = this.element.esn;
    this.originalElement.productId = this.element.productId;
    this.originalElement.partNum = this.element.partNum;
    this.originalElement.deviceStatusId = this.element.deviceStatusId;
    this.originalElement.assigneeId = this.element.assigneeId;
    this.originalElement.simId = this.element.simId;
    this.originalElement.active = this.originalElement.active;
  }

  updateElement() {
    this.service.updateDevice(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
        setTimeout(() => {
          this.alertify.confirm('Would you like to restore the original values?', () => {
            this.restoreOriginal();
          });
        }, 2000);
      });
  }

  insertElement() {
    this.service.addDevice(this.element)
      .subscribe((element: Device) => {
        this.alertify.success(this.elementLabel + ' added successfully');
        this.element = element;
      }, error => {
        this.alertify.error(error);
      });
  }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.haveValuesChanged() || this.element.id === 0) {
        if (this.element.id > 0) {
          this.updateElement();
        } else {
          this.insertElement();
        }
      } else {
        this.alertify.error('You haven\'t changed anything');
        this.editForm.control.markAsPristine();
        this.editForm.control.markAsUntouched();
      }
    }
  }

  deactivateElement(id: number, e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementTypeLabel + '?', () => {
      this.service.deactivateDevice(id)
        .subscribe(() => {
          this.router.navigate(['/' + this.elementRoute]);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.router.navigate(['/' + this.elementRoute]);
  }

  resetSelectables() {
    // this.element.productCapacityId = 0;
    // this.element.productModelId = 0;
  }

  setProductType(productType: number) {
    // this.element.productTypeId = productType;
    // this.resetSelectables();
    // this.markAsDirty();
    // this.testFormValidity();
  }

  setProductManufacturer(productManufacturer: number) {
    // this.element.productManufacturerId = productManufacturer;
    // this.resetSelectables();
    // this.markAsDirty();
    // this.testFormValidity();
  }

  setProductModel(productModel: number) {
    // this.element.productModelId = productModel;
    // this.markAsDirty();
    // this.testFormValidity();
    // // console.log('setProductModel');
  }

  setProductCapacity(productCapacity: number) {
    // this.element.productCapacityId = productCapacity;
    // this.markAsDirty();
    // this.testFormValidity();
  }

  markAsDirty() {
    this.editForm.control.markAsDirty();
  }

  isFormValid() {
    return false;
    // return this.element.productTypeId > 0 &&
    //   this.element.productCapacityId > 0 &&
    //   this.element.productManufacturerId > 0 &&
    //   this.element.productModelId > 0 &&
    //   this.element.partNum !== '';
  }

  testFormValidity() {
      this.formInvalid = !this.isFormValid();
  }

  haveValuesChanged() {
    return false;
    // return this.originalElement.partNum !== this.element.partNum ||
    //   this.originalElement.productCapacityId !== this.element.productCapacityId ||
    //   this.originalElement.productManufacturerId !== this.element.productManufacturerId ||
    //   this.originalElement.productTypeId !== this.element.productTypeId ||
    //   this.originalElement.productModelId !== this.element.productModelId ||
    //   this.originalElement.active !== this.element.active;
  }

  restoreOriginal() {
    // this.element.partNum = this.originalElement.partNum;
    // this.element.productCapacityId = this.originalElement.productCapacityId;
    // this.element.productManufacturerId = this.originalElement.productManufacturerId;
    // this.element.productTypeId = this.originalElement.productTypeId;
    // this.element.productModelId = this.originalElement.productModelId;
    // this.element.active = this.originalElement.active;
  }
}
