import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit, Input } from '@angular/core';
import { DeviceDate } from '../_models/devicedate';
import { DevicedateService } from '../_services/devicedate.service';

@Component({
  selector: 'app-devicedate-editable-row',
  templateUrl: './devicedate-editable-row.component.html',
  styleUrls: ['./devicedate-editable-row.component.css']
})
export class DevicedateEditableRowComponent implements OnInit {
  @Input() deviceDate: DeviceDate;

  deviceDateDate: Date;

  editState = false;

  constructor(private service: DevicedateService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.deviceDateDate = new Date(this.deviceDate.dateValue);
  }

  onEdit (e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.editState = !this.editState;
  }

}
