import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { DevicedateService } from '../_services/devicedate.service';
import { DeviceDate } from '../_models/devicedate';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-devicedate-add',
  templateUrl: './devicedate-add.component.html',
  styleUrls: ['./devicedate-add.component.css']
})
export class DevicedateAddComponent implements OnInit {
  @ViewChild('addForm') addForm: NgForm;
  @Input() deviceId: number;
  @Output() closeModal = new EventEmitter();

  selectedDate: Date;
  blankDate: Date = new Date('1900-01-01');
  newDeviceDate: DeviceDate = {
    id: 0,
    deviceId: 0,
    dateTypeId: 0,
    dateTypeName: '',
    dateValue: this.blankDate,
    active: true,
    createdBy: '',
    createdDate: this.blankDate,
    modifiedBy: '',
    modifiedDate: this.blankDate
  };

  isValid = false;

  constructor(private service: DevicedateService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.newDeviceDate.deviceId = this.deviceId;
  }

  addDeviceDate() {
    this.service.addDeviceDate(this.newDeviceDate)
    .subscribe((element: DeviceDate) => {
      this.alertify.success('Added successfully');
      this.close();
    }, error => {
      this.alertify.error(error);
    });
  }

  setDateType(dateType: number) {
    this.newDeviceDate.dateTypeId = dateType;
    // this.addForm.control.markAsDirty();
    this.testFormValid();
  }

  onValueChange(value: Date): void {
    // const pickedDate = this.datePipe.transform(value, 'MM/dd/yyyy');
    this.newDeviceDate.dateValue = value;
    // this.addForm.control.markAsDirty();
    this.testFormValid();
  }

  cancel(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.resetForm();
    this.close();
  }

  resetForm() {
    this.newDeviceDate.dateTypeId = 0;
    this.newDeviceDate.dateValue = null;
    this.selectedDate = null;
  }

  close() {
    this.closeModal.emit();
  }

  testFormValid() {
    this.isValid = this.newDeviceDate.dateTypeId > 0 && this.newDeviceDate.dateValue && this.newDeviceDate.dateValue !== this.blankDate;
  }

}
