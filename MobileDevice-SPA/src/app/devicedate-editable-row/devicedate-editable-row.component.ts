import { DatePipe } from '@angular/common';
import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DeviceDate } from '../_models/devicedate';
import { DevicedateService } from '../_services/devicedate.service';


@Component({
  selector: 'app-devicedate-editable-row',
  templateUrl: './devicedate-editable-row.component.html',
  styleUrls: ['./devicedate-editable-row.component.css']
})
export class DevicedateEditableRowComponent implements OnInit {
  @Input() deviceDate: DeviceDate;
  @Output() recordDeleted = new EventEmitter();

  deviceDateDate: Date;

  editState = false;

  constructor(private service: DevicedateService, private alertify: AlertifyService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.deviceDateDate = new Date(this.deviceDate.dateValue);
  }

  onEdit () {
    this.editState = !this.editState;
  }

  onUpdate () {
    this.service.updateDeviceDate(this.deviceDate)
    .subscribe(next => {
      this.alertify.success('Updated Successfully');
      this.editState = false;
      // this.deviceDate = deviceDate;
    }, error => {
      this.alertify.error(error);
    });
  }

  onDelete() {
    this.service.deactivateDeviceDate(this.deviceDate.id)
    .subscribe(() => {
      this.alertify.success('Deactivated Successfully');
      this.recordDeleted.emit();
    }, error => {
      this.alertify.error(error);
    });
  }

  onValueChange(value: Date): void {
    const pickedDate = this.datePipe.transform(value, 'MM/dd/yyyy');
    this.deviceDate.dateValue = value;
  }
}
