import { DeviceDateType } from './../../_models/devicedatetype';
import { DeviceDateTypeService } from './../../_services/devicedatetype.service';
import { Component, OnInit, Input, EventEmitter, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-devicedatetype-select',
  templateUrl: './devicedatetype-select.component.html',
  styleUrls: ['./devicedatetype-select.component.css']
})
export class DevicedatetypeSelectComponent implements OnInit {
  @Input() deviceId: number;
  @Input() selectedDeviceDateTypeId: number;
  @Input() showNoValue: boolean;
  @Input() noValueLabel = '.: Any :.';
  @Output() selectedOption = new EventEmitter();

  deviceDateTypes: DeviceDateType[];

  constructor(private service: DeviceDateTypeService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(changes: SimpleChanges) {
    if (changes['deviceId']) {
      this.loadList();
    }
  }

  ngOnInit() {
    this.loadList();
  }

  loadList() {
    this.service.getAvailableDeviceDateType(this.deviceId)
    .subscribe((res: DeviceDateType[]) => {
      this.deviceDateTypes = res;
    });
  }

  optionSelected(val: number) {
    this.selectedDeviceDateTypeId = val;
    this.selectedOption.emit(this.selectedDeviceDateTypeId);
    console.log(this.selectedDeviceDateTypeId);
  }

}
