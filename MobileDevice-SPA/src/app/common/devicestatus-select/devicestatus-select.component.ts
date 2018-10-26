import { PaginatedResult } from './../../_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { DeviceStatus } from './../../_models/DeviceStatus';
import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { DeviceStatusService } from 'src/app/_services/devicestatus.service';

@Component({
  selector: 'app-devicestatus-select',
  templateUrl: './devicestatus-select.component.html',
  styleUrls: ['./devicestatus-select.component.css']
})
export class DevicestatusSelectComponent implements OnInit {
  @Input() selectedStatus: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectOption = new EventEmitter();

  list: DeviceStatus[];
  filter = '';

  constructor(private service: DeviceStatusService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadList();
  }

  loadList() {
    this.service.getDeviceStatuses(1, 100, this.filter, 1)
      .subscribe((res: PaginatedResult<DeviceStatus[]>) => {
        this.list = res.result;
      });
  }

  optionSelected(val: number) {
    this.selectedStatus = val;
    this.selectOption.emit(this.selectedStatus);
    console.log('Status: ' + this.selectedStatus.toString());
  }

}
